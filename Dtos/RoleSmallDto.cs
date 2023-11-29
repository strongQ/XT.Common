using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Common.Dtos
{
    /// <summary>
    /// 角色
    /// </summary>
    public class RoleSmallDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Permission { get; set; }

        public int Level { get; set; }

        public string DataScope { get; set; }
    }
}
