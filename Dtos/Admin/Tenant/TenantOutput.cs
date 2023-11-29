using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using XT.Common.Enums;

namespace XT.Common.Dtos.Admin.Tenant
{
    public class TenantOutput
    {
        /// <summary>
        /// 租户名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 管理员
        /// </summary>
        public virtual string AdminName { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 机构Id
        /// </summary>
        public long OrgId { get; set; }

        /// <summary>
        /// 主机
        /// </summary>
        [MaxLength(128)]
        public string Host { get; set; }

        /// <summary>
        /// 租户类型
        /// </summary>
        public TenantTypeEnum TenantType { get; set; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 数据库连接
        /// </summary>
        [MaxLength(256)]
        public string Connection { get; set; }

        /// <summary>
        /// 数据库标识
        /// </summary>
        [MaxLength(64)]
        public string ConfigId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNo { get; set; } = 100;

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(128)]
        public string Remark { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public StatusEnum Status { get; set; } = StatusEnum.Enable;
    }
}
