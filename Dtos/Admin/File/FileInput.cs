
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using XT.Common.Attributes;
using XT.Common.Dtos.Admin.Util;

namespace XT.Common.Dtos.Admin.File
{
    public class FileInput : BaseIdInput
    {
    }

    public class AddFileInput : BaseIdInput
    {
        /// <summary>
        /// 提供者
        /// </summary>

        [MaxLength(128)]
        public string Provider { get; set; }

        /// <summary>
        /// 仓储名称
        /// </summary>

        [MaxLength(128)]
        [HeadDescription("存储标识")]
        public string BucketName { get; set; }

        /// <summary>
        /// 文件名称（上传时名称）
        /// </summary>文件名称
        [MaxLength(128)]
        [HeadDescription("文件名称")]
        public string FileName { get; set; }

        /// <summary>
        /// 文件后缀
        /// </summary>     
        [MaxLength(16)]
        [HeadDescription("文件后缀")]
        public string Suffix { get; set; }

        /// <summary>
        /// 存储路径
        /// </summary>
        [MaxLength(128)]
        public string FilePath { get; set; }

        /// <summary>
        /// 文件大小KB
        /// </summary>
        [MaxLength(16)]
        [HeadDescription("大小kb")]
        public string SizeKb { get; set; }

        /// <summary>
        /// 文件大小信息-计算后的
        /// </summary>
        [MaxLength(64)]
        public string SizeInfo { get; set; }

        /// <summary>
        /// 外链地址-OSS上传后生成外链地址方便前端预览
        /// </summary>      
        [MaxLength(128)]
        [HeadDescription("预览")]
        public string Url { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [HeadDescription("创建时间")]
        public virtual DateTime? CreateTime { get; set; }
    }

    public class PageFileInput : BasePageInput
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }

    public class DeleteFileInput : BaseIdInput
    {
    }
}
