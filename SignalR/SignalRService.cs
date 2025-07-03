// 注释


using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XT.Common.Interfaces;
using XT.Common.Models.SignalR;

namespace XT.Common.SignalR
{
    public class SignalRService : ISignalRService,IAsyncDisposable
    {
        

        private User _user;
        private HubConnection connection;
        public event EventHandler<string> ClosedEvent;
        public event EventHandler<string> ConnectedEvent;
        public event EventHandler<InformModel> MessageEvent;
        public event EventHandler<RemoteLog> RemoteLogEvent;
       

      
        public bool IsConnected { get; set; }
        /// <summary>
        /// 连接SignalR服务
        /// </summary>
        /// <param name="url"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> ConnectServer(string url, User user)
        {

            try
            {
                await StopServer();
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
                  // 告诉SignalR使用我们AOT兼容的序列化上下文，而不是依赖反射
                  options.PayloadSerializerOptions.TypeInfoResolver = SignalRJsonContext.Default;
              })
              .WithAutomaticReconnect(new RetryPolicy())
              .Build();
                    // 连接signalR
                    await connection.StartAsync();
                    connection.On("GetUser", async () =>
                    {
                        await connection.InvokeAsync<List<User>>("Login",
                       _user);
                    });

                    connection.Closed += Connection_Closed;
                    connection.Reconnecting += (e) =>
                    {
                        IsConnected = false;
                        return Task.CompletedTask;
                    };
                    connection.Reconnected += e =>
                    {
                        IsConnected = true;
                        ConnectedEvent?.Invoke(connection, "");


                        return Task.CompletedTask;
                    };
                }
               
                var users = await connection.InvokeAsync<List<User>>("Login",
                       user);

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



        /// <summary>
        /// 停止服务
        /// </summary>
        /// <returns></returns>
        public async Task<bool> StopServer()
        {
            try
            {
                if (connection == null)
                {
                    return false;
                }

                IsConnected = false;

                

                await connection.StopAsync();

                await connection.DisposeAsync();



                return true;
            }
            catch (Exception ex)
            {
                ClosedEvent?.Invoke(connection, ex.Message);
                return false;
            }
        }
        /// <summary>
        /// 关闭连接
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private Task Connection_Closed(Exception arg)
        {

            //await Task.Delay(new Random().Next(0, 5) * 1000);
            //await connection.StartAsync();
            // 目前不作处理，手动调用
            IsConnected = false;
           
            ClosedEvent?.Invoke(connection, arg.Message);
            return Task.CompletedTask;
        }

       
       
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public async Task<bool> WriteLog(RemoteLog log)
        {
            try
            {
                if (connection == null || connection.State != HubConnectionState.Connected)
                {
                    return false;
                }
                await connection.InvokeAsync("WriteLog",
                           log);
                return true;
            }
            catch (Exception ex)
            {
                ClosedEvent?.Invoke(connection, ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 写信息
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public async Task<bool> WriteMessage(InformModel log)
        {
            try
            {
                if (connection == null || connection.State != HubConnectionState.Connected)
                {
                    return false;
                }
                await connection.InvokeAsync("WriteMessage",
                           log);
                return true;
            }
            catch (Exception ex)
            {
                ClosedEvent?.Invoke(connection, ex.Message);
                return false;
            }
        }

      

        public async ValueTask DisposeAsync()
        {
            await StopServer();
        }
    }
}