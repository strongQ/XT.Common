using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using XT.Common.Dtos.Admin.Util;
using XT.Common.Enums;

namespace XT.Common.Dtos.Admin.Notice
{
    public class PageNoticeInput : BasePageInput
    {
        /// <summary>
        /// 标题
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// 类型（1通知 2公告）
        /// </summary>
        public virtual NoticeTypeEnum? Type { get; set; }
    }

    public class AddNoticeInput
    {
        /// <summary>
        /// 标题
        /// </summary>
        [Required, MaxLength(32)]
        public virtual string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Required]
        public virtual string Content { get; set; }

        /// <summary>
        /// 类型（1通知 2公告）
        /// </summary>
        public NoticeTypeEnum Type { get; set; }

        /// <summary>
        /// 发布人Id
        /// </summary>
        public long PublicUserId { get; set; }

        /// <summary>
        /// 发布人姓名
        /// </summary>
        [MaxLength(32)]
        public string PublicUserName { get; set; }

        /// <summary>
        /// 发布机构Id
        /// </summary>
        public long PublicOrgId { get; set; }

        /// <summary>
        /// 发布机构名称
        /// </summary>
        [MaxLength(64)]
        public string PublicOrgName { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime? PublicTime { get; set; }

        /// <summary>
        /// 撤回时间
        /// </summary>
        public DateTime? CancelTime { get; set; }

        /// <summary>
        /// 状态（0草稿 1发布 2撤回 3删除）
        /// </summary>
        public NoticeStatusEnum Status { get; set; }
    }

    public class UpdateNoticeInput : AddNoticeInput
    {
    }

    public class DeleteNoticeInput : BaseIdInput
    {
    }

    public class NoticeInput : BaseIdInput
    {
    }
}
