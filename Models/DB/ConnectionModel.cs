using System;
using System.Collections.Generic;
using System.Text;
using XT.Common.Enums;

namespace XT.Common.Models.DB
{
    public class ConnectionModel
    {
        public SqlEnum Type { get; set; }
        /// <summary>
        /// 标志
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 连接串
        /// </summary>
        public string ConnectionString { get; set; }
    }
}
