using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Common.Dtos.Admin.Role
{
    /// <summary>
    /// 角色列表输出参数
    /// </summary>
    public class RoleOutput
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; } = -1;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
    }
}
