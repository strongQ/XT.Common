using System;
using System.Collections.Generic;
using System.Text;
using XT.Common.Dtos.Admin.Util;

namespace XT.Common.Dtos.Admin.DataBase
{
    public class DbSearchInput : BasePageInput
    {
        /// <summary>
        /// 库ID
        /// </summary>
        public string ConfigId { get; set; }
        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName { get; set; }
    }
}
