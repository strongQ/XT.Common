using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace XT.Common.Dtos.Admin.SysServer
{
    public class ServerEnvInfo
    {
        /// <summary>
        /// 主机名称
        /// </summary>
        [DisplayName("主机名称")]
        public string HostName { get; set; }
        /// <summary>
        /// 操作系统
        /// </summary>
        [DisplayName("操作系统")]
        public string SystemOs { get; set; }
        /// <summary>
        /// 架构
        /// </summary>
        [DisplayName("架构")]
        public string OsArchitecture { get; set; }
        /// <summary>
        /// CPU核心
        /// </summary>
        [DisplayName("CPU核心")]
        public string ProcessorCount { get; set; }
        /// <summary>
        /// 外网IP
        /// </summary>
        [DisplayName("外网IP")]
        public string RemoteIp { get; set; }
        /// <summary>
        /// 本地地址
        /// </summary>
        [DisplayName("本地IP")]
        public string LocalIp { get; set; }
        /// <summary>
        /// 网站根目录
        /// </summary>
        [DisplayName("根目录")]
        public string Wwwroot { get; set; }
        /// <summary>
        /// Stage环境
        /// </summary>
        [DisplayName("Stage环境")]
        public string Stage { get; set; }
    }
}
