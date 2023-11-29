using System;
using System.Collections.Generic;
using System.Text;
using XT.Common.Enums;

namespace XT.Common.Models.SignalR
{
    public class User
    {

        public string Name { get; set; }
        public string ID { get; set; }


        public string ConnectionID { get; set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        public DeviceEnum Type { get; set; }
        /// <summary>
        /// 用户标记
        /// </summary>
        public string Flag { get { return $"{ID}*{Type}"; } }
        /// <summary>
        /// IP地址
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 是否重连
        /// </summary>
        public bool Reconnect { get; set; }


        public override string ToString()
        {
            return $"ID:{ID},Name:{Name},IP:{IP},Type:{Type}";
        }
    }
}
