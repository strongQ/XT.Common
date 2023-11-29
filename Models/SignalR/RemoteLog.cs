using System;
using System.Collections.Generic;
using System.Text;
using XT.Common.Enums;

namespace XT.Common.Models.SignalR
{
    public class RemoteLog
    {
        public string Title { get; set; }

        public string Flag { get; set; }

        public string Content { get; set; }

        public LogEnum Type { get; set; }
        /// <summary>
        /// 保存到数据库
        /// </summary>
        public bool IsToDB { get; set; }
        /// <summary>
        /// 推向Remote
        /// </summary>
        public bool IsToRemote { get; set; }

        public DateTime CreateTime { get; set; }

        public string ID { get; set; } = Guid.NewGuid().ToString();
    }
}
