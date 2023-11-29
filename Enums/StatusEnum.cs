using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace XT.Common.Enums
{
    /// <summary>
    /// 通用状态枚举
    /// </summary>
    public enum StatusEnum
    {
        /// <summary>
        /// 启用
        /// </summary>
        [Description("启用")]
        Enable = 1,

        /// <summary>
        /// 停用
        /// </summary>
        [Description("停用")]
        Disable = 2,
    }
    /// <summary>
    /// 成功枚举
    /// </summary>
    public enum SuccessEnum
    {
        /// <summary>
        /// 无操作
        /// </summary>
        [Description("无操作")]
        Info = 0,
        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Success = 1,
        /// <summary>
        /// 失败
        /// </summary>
        [Description("失败")]
        Fail = 2


    }
}
