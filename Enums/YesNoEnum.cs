using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace XT.Common.Enums
{
    /// <summary>
    /// 是否枚举
    /// </summary>
    public enum YesNoEnum
    {
        /// <summary>
        /// 是
        /// </summary>
        [Description("是")]
        Y = 1,

        /// <summary>
        /// 否
        /// </summary>
        [Description("否")]
        N = 2
    }
}
