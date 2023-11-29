using System;
using System.Collections.Generic;
using System.Text;
using XT.Common.Enums;
using XT.Common.Models.SignalR;

namespace XT.Common.Models.Connect
{
    /// <summary>
    /// 连接数据
    /// </summary>
    public class ConnectGroupData
    {
        /// <summary>
        /// 通知类型
        /// </summary>
        public InformTypeEnum InformType { get; set; }

        public string Message { get; set; }
        /// <summary>
        /// 连接的客户端ID
        /// </summary>
        public string ClientID { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        public DeviceEnum DeviceType { get; set; }
    }
}
