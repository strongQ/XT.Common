// 注释

using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Channels; // 引入Channel所需命名空间
using System.Threading.Tasks;
using XT.Common.Converters;
using XT.Common.Interfaces;
using XT.Common.Models.SignalR;

namespace XT.Common.SignalR
{
    public class SignalRService : ISignalRService, IAsyncDisposable
    {
        // ====================================================================
        // ======================= 新增部分：消息队列核心 =======================
        // ====================================================================

        /// <summary>
        /// 定义一个内部记录类型，用于封装要发送的消息及其目标Hub方法名
        /// </summary>
        private record QueuedMessage(string MethodName, object Payload);

        /// <summary>
        /// 线程安全的消息通道（队列），用于缓冲待发送的消息
        /// </summary>
        private readonly Channel<QueuedMessage> _messageChannel;

        /// <summary>
        /// 后台处理队列的任务
        /// </summary>
        private readonly Task _processingTask;

        // ====================================================================
        // ======================= 原有成员变量和事件 ==========================
        // ====================================================================
        private User _user;
        private HubConnection connection;
        public event EventHandler<string> ClosedEvent;
        public event EventHandler<string> ConnectedEvent;
        public event EventHandler<InformModel> MessageEvent;
        public event EventHandler<RemoteLog> RemoteLogEvent;

        public bool IsConnected { get; set; }


        /// <summary>
        /// 构造函数：初始化队列并启动后台处理任务
        /// </summary>
        public SignalRService()
        {
            // 创建一个无界的Channel，容量不限
            _messageChannel = Channel.CreateUnbounded<QueuedMessage>();
            // 启动一个长期运行的后台任务来处理队列中的消息
            // 使用 _ = 表示我们特意“发射后不管”，因为它会在整个服务生命周期内运行
            _processingTask = ProcessMessageQueueAsync();
        }

        /// <summary>
        /// 连接SignalR服务
        /// </summary>
        public async Task<bool> ConnectServer(string url, User user)
        {
            try
            {
                await StopServer(); // 保持原有逻辑，先尝试停止旧连接
                if (connection != null && connection.State == HubConnectionState.Connecting)
                {
                    return false;
                }
                if (connection == null || connection.State != HubConnectionState.Connected)
                {
                    _user = user;
                    connection = new HubConnectionBuilder()
                  .WithUrl($"{url}/MessageHub", options =>
                  {
                      options.AccessTokenProvider = async () => await Task.FromResult($"{user.ID}*{user.Name}*{user.Type}");
                  }).AddJsonProtocol(options =>
                  {
                      options.PayloadSerializerOptions.TypeInfoResolver = SignalRJsonContext.Default;
                      // 把所有你需要的选项都配置在这里
                      options.PayloadSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                      options.PayloadSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

                      // 把你的转换器添加到SignalR客户端的序列化器中
                      options.PayloadSerializerOptions.Converters.Add(new DateTimeParseConverter());
                  })
                  .WithAutomaticReconnect(new RetryPolicy()) // 自动重连依然非常重要
                  .Build();

                    // 注册生命周期事件
                    connection.Closed += Connection_Closed;
                    connection.Reconnecting += (e) =>
                    {
                        IsConnected = false;
                        // 这里不需要做什么，后台处理任务会自动等待
                        return Task.CompletedTask;
                    };
                    connection.Reconnected += e =>
                    {
                        IsConnected = true;
                        ConnectedEvent?.Invoke(connection, "");
                        // 连接恢复，后台处理任务会自动感知并开始发送积压的消息
                        return Task.CompletedTask;
                    };

                    // 启动连接
                    await connection.StartAsync();

                    // 注册服务器推送的回调（保持不变）
                    connection.On("GetUser", async () =>
                    {
                        await connection.InvokeAsync<List<User>>("Login", _user);
                    });

                    if (MessageEvent != null)
                    {
                        connection.On<InformModel>("InformMessage", (inform) =>
                        {
                            MessageEvent?.Invoke(connection, inform);
                        });
                    }
                    if (RemoteLogEvent != null)
                    {
                        connection.On<RemoteLog>("ReadLog", (log) =>
                        {
                            RemoteLogEvent?.Invoke(connection, log);
                        });
                    }
                }

                await connection.InvokeAsync<List<User>>("Login", user);

                ConnectedEvent?.Invoke(connection, "");
                IsConnected = true;
                return true;
            }
            catch (Exception ex)
            {
                ClosedEvent?.Invoke(connection, ex.Message);
                return false;
            }
        }

        // ====================================================================
        // ======================= 修改部分：写入消息到队列 =====================
        // ====================================================================

        /// <summary>
        /// 写日志 (修改后：写入队列)
        /// </summary>
        public async Task<bool> WriteLog(RemoteLog log)
        {
            try
            {
                // 不再检查连接状态，直接将消息写入Channel
                await _messageChannel.Writer.WriteAsync(new QueuedMessage("WriteLog", log));
                return true; // 表示成功入队
            }
            catch (Exception)
            {
                // 写入Channel失败通常只在程序关闭时发生，这里返回false是合理的
                return false;
            }
        }

        /// <summary>
        /// 写信息 (修改后：写入队列)
        /// </summary>
        public async Task<bool> WriteMessage(InformModel log)
        {
            try
            {
                // 不再检查连接状态，直接将消息写入Channel
                await _messageChannel.Writer.WriteAsync(new QueuedMessage("WriteMessage", log));
                return true; // 表示成功入队
            }
            catch (Exception)
            {
                return false;
            }
        }

        // ====================================================================
        // ======================= 新增部分：后台队列处理器 =====================
        // ====================================================================

        /// <summary>
        /// 持续从消息队列中读取并发送消息的后台任务。
        /// </summary>
        private async Task ProcessMessageQueueAsync()
        {
            // ReadAllAsync会一直等待，直到Channel被标记为完成。
            await foreach (var message in _messageChannel.Reader.ReadAllAsync())
            {
                // 循环等待，直到连接建立并处于Connected状态
                // 这样可以处理首次连接慢或中途断线重连的各种情况
                while (connection == null || connection.State != HubConnectionState.Connected)
                {
                    // 可以添加日志来观察等待状态
                    //_logger.LogTrace("SignalR connection not ready. Waiting...");
                    await Task.Delay(500); // 等待500毫秒再检查，避免CPU空转
                }

                try
                {
                    // 连接正常，发送消息
                    await connection.InvokeAsync(message.MethodName, message.Payload);
                }
                catch (Exception ex)
                {
                    // 如果在发送时发生异常（例如，连接恰好在这一刻断开）
                    // 可以记录错误日志。
                    // 策略选择：
                    // 1. 放弃消息（当前实现）
                    // 2. 将消息重新入队（有风险，可能导致死循环）
                    // 3. 记录到持久化存储（如文件或数据库）进行补偿
                    ClosedEvent?.Invoke(connection, $"Failed to send message from queue: {ex.Message}");
                }
            }
        }


        // ====================================================================
        // ======================= 其他方法（部分有修改） =======================
        // ====================================================================

        /// <summary>
        /// 停止服务
        /// </summary>
        public async Task<bool> StopServer()
        {
            try
            {
                if (connection == null)
                {
                    return true; // 原来是false，改为true表示已经处于停止状态
                }

                IsConnected = false;
                await connection.StopAsync();
                // 在DisposeAsync中处理Dispose，这里不再调用
                return true;
            }
            catch (Exception ex)
            {
                ClosedEvent?.Invoke(connection, ex.Message);
                return false;
            }
        }

        private Task Connection_Closed(Exception arg)
        {
            IsConnected = false;
            // arg可能为null，需要检查
            ClosedEvent?.Invoke(connection, arg?.Message ?? "Connection closed without error.");
            return Task.CompletedTask;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public async ValueTask DisposeAsync()
        {
            // 1. 标记Channel为完成状态，不再接受新消息。
            //    后台的 await foreach 循环会在处理完所有已有消息后优雅地退出。
            _messageChannel.Writer.Complete();

            // 2. 等待后台处理任务结束
            await _processingTask;

            // 3. 停止并释放SignalR连接
            if (connection != null)
            {
                await connection.DisposeAsync();
            }
        }
    }
}