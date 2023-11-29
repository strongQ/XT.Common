using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Common.Dtos.Admin.CodeGen
{
    /// <summary>
    /// 数据库
    /// </summary>
    public class DatabaseOutput
    {
        /// <summary>
        /// 库定位器名
        /// </summary>
        public string ConfigId { get; set; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public int DbType { get; set; }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
    }
}
