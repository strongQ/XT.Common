
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XT.Common.Models.SignalR;

namespace XT.Common.Hubs
{
    public interface IMessageClient
    {
        //发送通知消息
        Task InformMessage(InformModel inform);
        /// <summary>
        /// 读取远程log
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        Task ReadLog(RemoteLog log);
        /// <summary>
        /// 关闭连接
        /// </summary>
        /// <param name="close">原因</param>
        /// <returns></returns>
        Task Close(CloseModel close);
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <returns></returns>
        Task GetUser();
    }
}
