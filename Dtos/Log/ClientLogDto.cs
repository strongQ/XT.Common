using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Common.Dtos.Log
{
    public class ClientLogDto
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 客户端ID
        /// </summary>
        public string ClientID { get; set; }
        /// <summary>
        /// 客户端名称
        /// </summary>
        public string ClientName { get; set; }
        /// <summary>
        /// 是否记录
        /// </summary>
        public bool IsRecord { get; set; }

        /// <summary>
        /// 日志标题
        /// </summary>
        public string LogTitle { get; set; }

        /// <summary>
        /// 日志内容
        /// </summary>
        public string LogContent { get; set; }

        /// <summary>
        /// 日志内容
        /// </summary>
        public string LogType { get; set; }

        public string ID { get; set; }
    }
}
