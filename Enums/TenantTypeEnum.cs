﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace XT.Common.Enums
{
    /// <summary>
    /// 租户类型枚举
    /// </summary>
    public enum TenantTypeEnum
    {
        /// <summary>
        /// Id隔离
        /// </summary>
        [Description("Id隔离")]
        Id = 0,

        /// <summary>
        /// 库隔离
        /// </summary>
        [Description("库隔离")]
        Db = 1,
    }
}
