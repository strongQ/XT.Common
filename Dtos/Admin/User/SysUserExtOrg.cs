using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using XT.Common.Dtos.Admin.Org;
using XT.Common.Dtos.Admin.Pos;

namespace XT.Common.Dtos.Admin.User
{
    public class SysUserExtOrg
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 机构Id
        /// </summary>
        public long OrgId { get; set; }

        /// <summary>
        /// 机构
        /// </summary>

        public AddOrgInput SysOrg { get; set; }

        /// <summary>
        /// 职位Id
        /// </summary> 
        public long PosId { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        public AddPosInput SysPos { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        [MaxLength(32)]
        public string JobNum { get; set; }

        /// <summary>
        /// 职级
        /// </summary>
        [MaxLength(32)]
        public string PosLevel { get; set; }

        /// <summary>
        /// 入职日期
        /// </summary>
        public DateTime? JoinDate { get; set; }
    }
}
