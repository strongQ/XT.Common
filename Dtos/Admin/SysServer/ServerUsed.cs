using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace XT.Common.Dtos.Admin.SysServer
{
    public class ServerUsed
    {
        /// <summary>
        /// 空闲内存
        /// </summary>
        [DisplayName("空闲内存")]
        public string FreeRam { get; set; }
        /// <summary>
        /// 已用内存
        /// </summary>
        [DisplayName("已用内存")]
        public string UsedRam { get; set; }
        /// <summary>
        /// 总内存
        /// </summary>
        [DisplayName("总内存")]
        public string TotalRam { get; set; }
        /// <summary>
        /// 内存使用率
        /// </summary>
        [DisplayName("内存使用率")]
        public string RamRate { get; set; }
        /// <summary>
        /// Cpu使用率
        /// </summary>
        [DisplayName("CPU使用率")]
        public string CpuRate { get; set; }
        /// <summary>
        /// 启动时间
        /// </summary>
        [DisplayName("启动时间")]
        public string StartTime { get; set; }
        /// <summary>
        /// 运行时间
        /// </summary>
        [DisplayName("运行时间")]
        public string RunTime { get; set; }
    }
}
