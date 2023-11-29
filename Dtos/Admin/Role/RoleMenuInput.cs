using System;
using System.Collections.Generic;
using System.Text;
using XT.Common.Dtos.Admin.Util;

namespace XT.Common.Dtos.Admin.Role
{
    /// <summary>
    /// 授权角色菜单
    /// </summary>
    public class RoleMenuInput : BaseIdInput
    {
        /// <summary>
        /// 菜单Id集合
        /// </summary>
        public List<long> MenuIdList { get; set; }
    }
}
