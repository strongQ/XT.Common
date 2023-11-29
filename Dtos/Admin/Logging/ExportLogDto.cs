using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;

namespace XT.Common.Dtos.Admin.Logging
{
    /// <summary>
    /// 导出日志数据
    /// </summary>
    public class ExportLogDto
    {
        /// <summary>
        /// 记录器类别名称
        /// </summary>
        public string LogName { get; set; }

        /// <summary>
        /// 日志级别
        /// </summary>
        public string LogLevel { get; set; }

        /// <summary>
        /// 事件Id
        /// </summary>
        public string EventId { get; set; }

        /// <summary>
        /// 日志消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 异常对象
        /// </summary>
        public string Exception { get; set; }

        /// <summary>
        /// 当前状态值
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// 日志记录时间
        /// </summary>
        public DateTime LogDateTime { get; set; }

        /// <summary>
        /// 线程Id
        /// </summary>
        public int ThreadId { get; set; }

        /// <summary>
        /// 请求跟踪Id
        /// </summary>
        public string TraceId { get; set; }
    }
}
