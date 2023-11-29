using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace XT.Common.Enums
{
    /// <summary>
    /// 通知公告状类型枚举
    /// </summary>
    public enum NoticeTypeEnum
    {
        /// <summary>
        /// 通知
        /// </summary>
        [Description("通知")]
        NOTICE = 1,

        /// <summary>
        /// 公告
        /// </summary>
        [Description("公告")]
        ANNOUNCEMENT = 2,
    }
}
