using System;
using System.Collections.Generic;
using System.Text;
using XT.Common.Dtos.Admin.Util;

namespace XT.Common.Dtos.Admin.User
{
    public class UserExtOrgInput : BaseIdInput
    {
        /// <summary>
        /// 机构Id
        /// </summary>
        public long OrgId { get; set; }

        /// <summary>
        /// 职位Id
        /// </summary>
        public long PosId { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        public string JobNum { get; set; }

        /// <summary>
        /// 职级
        /// </summary>
        public string PosLevel { get; set; }

        /// <summary>
        /// 入职日期
        /// </summary>
        public DateTime? JoinDate { get; set; }
    }
}
