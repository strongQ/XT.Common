using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Common.Services
{
    /// <summary>
    /// 消息委托
    /// </summary>
    /// <param name="sendPageTag"></param>
    /// <param name="recievePageTag"></param>
    /// <param name="msg"></param>
    public delegate void MsgDelegate(string sendPageTag, string recievePageTag, string msg);
    public interface IMsgService
    {
        public event MsgDelegate OnMsg;
        void SendMsg(string sendPageTag, string recievePageTag, string msg);

    }
}
