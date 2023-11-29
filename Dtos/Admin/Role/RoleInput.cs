using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using XT.Common.DataValidation;
using XT.Common.Dtos.Admin.Util;
using XT.Common.Enums;

namespace XT.Common.Dtos.Admin.Role
{
    public class RoleInput : BaseIdInput
    {
        /// <summary>
        /// 状态
        /// </summary>
        public virtual StatusEnum Status { get; set; } = StatusEnum.Enable;
    }

    public class PageRoleInput : BasePageInput
    {
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public virtual string Code { get; set; }
    }

    public class AddRoleInput
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "角色名称不能为空")]
        public string Name { get; set; }



        /// <summary>
        /// 编码
        /// </summary>
        [MaxLength(64)]
        public string Code { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNo { get; set; } = 100;

        /// <summary>
        /// 数据范围（1全部数据 2本部门及以下数据 3本部门数据 4仅本人数据 5自定义数据）
        /// </summary>
        public DataScopeEnum DataScope { get; set; } = DataScopeEnum.All;

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(128)]
        public string Remark { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public StatusEnum Status { get; set; } = StatusEnum.Enable;

        /// <summary>
        /// 菜单Id集合
        /// </summary>
        public List<long> MenuIdList { get; set; }
    }

    public class UpdateRoleInput : AddRoleInput
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Required(ErrorMessage = "Id不能为空")]
        [DataValidation(ValidationTypes.Numeric)]
        public virtual long Id { get; set; }
    }

    public class DeleteRoleInput : BaseIdInput
    {
    }
}
