using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XT.Common.Models.SignalR;

namespace XT.Common.Interfaces
{
    public interface ISignalRService
    {
        public event EventHandler<string> ClosedEvent;
        public event EventHandler<string> ConnectedEvent;
        public event EventHandler<InformModel> MessageEvent;
        public event EventHandler<RemoteLog> RemoteLogEvent;

        public bool IsConnected { get; set; }
        /// <summary>
        /// 连接服务
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="user">用户</param>
        /// <returns>登录用户</returns>
        Task<bool> ConnectServer(string url, User user);
        /// <summary>
        /// 停止服务
        /// </summary>
        /// <returns></returns>
        Task<bool> StopServer();

      

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        Task<bool> WriteLog(RemoteLog log);
        /// <summary>
        /// 写信息
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        Task<bool> WriteMessage(InformModel log);
    }
}
