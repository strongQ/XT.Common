
using Mapster;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using XT.Common.Attributes;
using XT.Common.Dtos.Admin.Util;
using XT.Common.Enums;

namespace XT.Common.Dtos.Admin.Role
{
    public class SysRole : BaseDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required, MaxLength(64)]
        [HeadDescription("名称")]
        public virtual string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [MaxLength(64)]
        [HeadDescription("编码")]
        public string Code { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [HeadDescription("排序")]
        public int OrderNo { get; set; } = 100;

        /// <summary>
        /// 数据范围（1全部数据 2本部门及以下数据 3本部门数据 4仅本人数据 5自定义数据）
        /// </summary>
        [HeadDescription("数据范围")]
        public DataScopeEnum DataScope { get; set; } = DataScopeEnum.All;

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(128)]
        public string Remark { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [HeadDescription("状态")]
        public StatusEnum Status { get; set; } = StatusEnum.Enable;
    }



}
