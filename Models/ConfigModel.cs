using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Common.Models
{
    public class ConfigModel
    {
        /// <summary>
        /// 远程地址1
        /// </summary>
        public string RemoteAddress { get; set; }
        /// <summary>
        /// 远程地址2
        /// </summary>
        public string RemoteAddressPro { get; set; }

        /// <summary>
        /// 数据库地址
        /// </summary>
        public string DB { get; set; }
        /// <summary>
        /// 第二个数据库地址
        /// </summary>

        public string DB1 { get; set; }
        /// <summary>
        /// sql模板
        /// </summary>
        public string SqlTemplate { get; set; }


    }
}
