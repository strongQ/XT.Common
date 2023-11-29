
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using XT.Common.Attributes;
using XT.Common.Dtos.Admin.Util;
using XT.Common.Enums;

namespace XT.Common.Dtos.Admin.Dict
{
    public class DictTypeInput : BaseIdInput
    {
        /// <summary>
        /// 状态
        /// </summary>
        public StatusEnum Status { get; set; }
    }

    public class PageDictTypeInput : BasePageInput
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
    }

    public class AddDictTypeInput : BaseIdInput
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required, MaxLength(64)]
        [HeadDescription("字典名称")]
        public virtual string Name { get; set; }

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
        [MaxLength(256)]
        [HeadDescription("备注")]
        public string Remark { get; set; }


    }

    public class UpdateDictTypeInput : AddDictTypeInput
    {
    }

    public class DeleteDictTypeInput : BaseIdInput
    {
    }

    public class GetDataDictTypeInput
    {
        /// <summary>
        /// 编码
        /// </summary>
        [Required(ErrorMessage = "字典类型编码不能为空")]
        public string Code { get; set; }
    }
}
