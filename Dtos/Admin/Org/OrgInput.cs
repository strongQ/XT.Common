
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using XT.Common.Attributes;
using XT.Common.Dtos.Admin.Util;
using XT.Common.Enums;
using XT.Common.Interfaces;

namespace XT.Common.Dtos.Admin.Org
{
    public class OrgInput : BasePageInput
    {
        public long Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
    }

    public class AddOrgInput : BaseIdInput,IRender
    {
        /// <summary>
        /// 租户Id
        /// </summary>
        public virtual long? TenantId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [HeadDescription("机构名称")]
        [Required(ErrorMessage = "机构名称不能为空")]
        public string Name { get; set; }

        // <summary>
        /// 父Id
        /// </summary>
        public long Pid { get; set; }


        /// <summary>
        /// 编码
        /// </summary>
        [HeadDescription("机构编码")]
        [MaxLength(64)]
        public string Code { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [HeadDescription("排序")]
        public int OrderNo { get; set; } = 100;

        /// <summary>
        /// 备注
        /// </summary>
        [HeadDescription("备注")]
        [MaxLength(128)]
        public string Remark { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [HeadDescription("状态")]
        public StatusEnum Status { get; set; } = StatusEnum.Enable;

        /// <summary>
        /// 机构子项
        /// </summary>
        public List<AddOrgInput> Children { get; set; }
        public bool IsRender { get; set; }
    }

    public class UpdateOrgInput : AddOrgInput
    {
    }

    public class DeleteOrgInput : BaseIdInput
    {
    }
}
