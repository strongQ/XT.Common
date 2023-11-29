
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using XT.Common.Attributes;
using XT.Common.DataValidation;
using XT.Common.Dtos.Admin.Util;
using XT.Common.Enums;

namespace XT.Common.Dtos.Admin.Dict
{
    public class DictDataInput : BaseIdInput
    {
        /// <summary>
        /// 状态
        /// </summary>
        public StatusEnum Status { get; set; }
    }

    public class PageDictDataInput : BasePageInput
    {
        /// <summary>
        /// 字典类型Id
        /// </summary>
        public long DictTypeId { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
    }

    public class AddDictDataInput : BaseIdInput
    {
        /// <summary>
        /// 字典类型Id
        /// </summary>
        public long DictTypeId { get; set; }

        /// <summary>
        /// 字典类型
        /// </summary>
        public AddDictTypeInput DictType { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [Required, MaxLength(128)]
        [HeadDescription("字典值")]
        public virtual string Value { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [Required, MaxLength(64)]
        [HeadDescription("字典编码")]
        public virtual string Code { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [HeadDescription("状态")]
        public StatusEnum Status { get; set; } = StatusEnum.Enable;
        /// <summary>
        /// 排序
        /// </summary>
        [HeadDescription("排序")]
        public int OrderNo { get; set; } = 100;

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(128)]
        [HeadDescription("备注")]
        public string Remark { get; set; }


    }

    public class UpdateDictDataInput : AddDictDataInput
    {
    }

    public class DeleteDictDataInput : BaseIdInput
    {
    }

    public class GetDataDictDataInput
    {
        /// <summary>
        /// 字典类型Id
        /// </summary>
        [Required(ErrorMessage = "字典类型Id不能为空"), DataValidation(ValidationTypes.Numeric)]
        public long DictTypeId { get; set; }
    }

    public class QueryDictDataInput
    {
        /// <summary>
        /// 编码
        /// </summary>
        [Required(ErrorMessage = "字典唯一编码不能为空")]
        public string Code { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }
    }
}
