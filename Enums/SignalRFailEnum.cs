using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Common.Enums
{
    public enum SignalRFailEnum
    {
        /// <summary>
        /// 相同用户
        /// </summary>
        Same,
        /// <summary>
        /// 通用错误
        /// </summary>
        Common,
        /// <summary>
        /// 关闭，客户端重启
        /// </summary>
        Close
    }
}
