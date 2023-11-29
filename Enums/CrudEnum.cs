using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace XT.Common.Enums
{
    /// <summary>
    /// 增删改查
    /// </summary>
    public enum CrudEnum
    {
        [Description("新增")]
        Add,
        [Description("编辑")]
        Edit,
        [Description("详情")]
        Detail,
        [Description("删除")]
        Delete
    }
}
