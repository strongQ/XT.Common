using System;
using System.Collections.Generic;
using System.Text;
using XT.Common.Dtos.Admin.Util;

namespace XT.Common.Dtos.Admin.Role
{
    /// <summary>
    /// 授权角色机构
    /// </summary>
    public class RoleOrgInput : BaseIdInput
    {
        /// <summary>
        /// 数据范围
        /// </summary>
        public int DataScope { get; set; }

        /// <summary>
        /// 机构Id集合
        /// </summary>
        public List<long> OrgIdList { get; set; }
    }
}
