
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using XT.Common.Attributes;
using XT.Common.Dtos.Admin.Util;
using XT.Common.Enums;

namespace XT.Common.Dtos.Admin.Menu
{
    public class MenuInput : BasePageInput
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 菜单类型（1目录 2菜单 3按钮）
        /// </summary>
        public MenuTypeEnum? Type { get; set; }
    }

    public class AddMenuInput : BaseDto
    {
        /// <summary>
        /// 分组名称
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "菜单名称不能为空")]
        [HeadDescription("名称")]
        public string Title { get; set; }

        /// <summary>
        /// 父Id
        /// </summary>
        public long Pid { get; set; }

        /// <summary>
        /// 菜单类型（1目录 2菜单 3按钮）
        /// </summary>
        [HeadDescription("类型")]
        public MenuTypeEnum Type { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(64)]
        public string Name { get; set; }

        /// <summary>
        /// 路由地址
        /// </summary>
        [MaxLength(128)]
        [HeadDescription("路径")]
        public string Path { get; set; }

        /// <summary>
        /// 组件路径
        /// </summary>
        [MaxLength(128)]
        [Description("组件路径")]
        public string Component { get; set; }

        /// <summary>
        /// 重定向
        /// </summary>
        [MaxLength(128)]
        public string Redirect { get; set; }

        /// <summary>
        /// 权限标识
        /// </summary>
        [MaxLength(128)]
        [HeadDescription("权限")]
        public string Permission { get; set; }



        /// <summary>
        /// 图标
        /// </summary>
        [MaxLength(128)]
        public string Icon { get; set; }

        /// <summary>
        /// 是否内嵌
        /// </summary>
        public bool IsIframe { get; set; }

        /// <summary>
        /// 外链链接
        /// </summary>
        [MaxLength(256)]
        public string OutLink { get; set; }

        /// <summary>
        /// 是否隐藏
        /// </summary>
        public bool IsHide { get; set; }

        /// <summary>
        /// 是否缓存
        /// </summary>
        public bool IsKeepAlive { get; set; } = true;

        /// <summary>
        /// 是否固定
        /// </summary>
        public bool IsAffix { get; set; }

        /// <summary>
        /// 排序
        /// </summary>

        public int OrderNo { get; set; } = 100;

        /// <summary>
        /// 状态
        /// </summary>
        [HeadDescription("状态")]

        public StatusEnum Status { get; set; } = StatusEnum.Enable;

        /// <summary>
        /// 备注
        /// </summary>

        [MaxLength(256)]
        [Description("备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 菜单子项
        /// </summary>

        public List<AddMenuInput> Children { get; set; } = new List<AddMenuInput>();
    }

    public class UpdateMenuInput : AddMenuInput
    {
    }

    public class DeleteMenuInput : BaseIdInput
    {
    }
}
