using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using XT.Common.Dtos.Admin.Util;
using XT.Common.Enums;

namespace XT.Common.Dtos.Admin.Config
{
    public class ConfigInput : BaseIdInput
    {
    }

    public class PageConfigInput : BasePageInput
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 分组编码
        /// </summary>
        public string GroupCode { get; set; }
    }

    public class AddConfigInput : BaseIdInput
    {
        /// <summary>
        /// 名称
        /// </summary>    
        [Required, MaxLength(64)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [MaxLength(64)]
        public string Code { get; set; }

        /// <summary>
        /// 属性值
        /// </summary>
        [MaxLength(64)]
        public string Value { get; set; }

        /// <summary>
        /// 是否是内置参数（Y-是，N-否）
        /// </summary>
        public YesNoEnum SysFlag { get; set; }

        /// <summary>
        /// 分组编码
        /// </summary>
        [MaxLength(64)]
        public string GroupCode { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNo { get; set; } = 100;

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(256)]
        public string Remark { get; set; }
    }

    public class UpdateConfigInput : AddConfigInput
    {
    }

    public class DeleteConfigInput : BaseIdInput
    {
    }
}
