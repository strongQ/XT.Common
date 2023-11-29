using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Common.Models.SignalR
{
    public class InformModel
    {
        /// <summary>
        /// 发送者
        /// </summary>
        public string Sender { get; set; }
        /// <summary>
        /// 消息体
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public InformTypeEnum Type { get; set; }
        /// <summary>
        /// 约定的Tag
        /// </summary>
        public int ContractTag { get; set; }
    }
}
