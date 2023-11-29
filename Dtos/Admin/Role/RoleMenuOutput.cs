using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Common.Dtos.Admin.Role
{
    /// <summary>
    /// 角色菜单输出参数
    /// </summary>
    public class RoleMenuOutput
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Title { get; set; }
    }
}
