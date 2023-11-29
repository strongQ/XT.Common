
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using XT.Common.Attributes;
using XT.Common.Dtos.Admin.Util;
using XT.Common.Enums;

namespace XT.Common.Dtos.Admin.Pos
{
    public class PosInput : BasePageInput
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

    public class AddPosInput : BaseDto
    {
        /// <summary>
        /// 职位名称
        /// </summary>
        [Required(ErrorMessage = "职位名称不能为空")]
        [HeadDescription("职位名称")]
        public string Name { get; set; }



        /// <summary>
        /// 编码
        /// </summary>
        [MaxLength(64)]
        [HeadDescription("职位编码")]
        public string Code { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [HeadDescription("排序")]
        public int OrderNo { get; set; } = 100;

        /// <summary>
        /// 状态
        /// </summary>
        [HeadDescription("状态")]
        public StatusEnum Status { get; set; } = StatusEnum.Enable;

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(128)]
        [HeadDescription("备注")]
        public string Remark { get; set; }


    }

    public class UpdatePosInput : AddPosInput
    {
    }

    public class DeletePosInput : BaseIdInput
    {
    }
}
