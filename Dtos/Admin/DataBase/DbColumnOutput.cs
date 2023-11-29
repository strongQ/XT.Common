
using System;
using System.Collections.Generic;
using System.Text;
using XT.Common.Attributes;
using XT.Common.Dtos.Admin.Util;

namespace XT.Common.Dtos.Admin.DataBase
{
    public class DbColumnOutput : BaseIdInput
    {


        public string TableName { get; set; }

        public int TableId { get; set; }

        [HeadDescription("字段名")]
        public string DbColumnName { get; set; }

        /// <summary>
        /// 新增属性，以前的列名称
        /// </summary>
        public string OldColumnName { get; set; }

        public string PropertyName { get; set; }
        [HeadDescription("数据类型")]
        public string DataType { get; set; }

        public object PropertyType { get; set; }
        [HeadDescription("长度")]
        public int Length { get; set; }


        public string DefaultValue { get; set; }
        [HeadDescription("可空")]
        public bool IsNullable { get; set; }
        [HeadDescription("自增")]
        public bool IsIdentity { get; set; }
        [HeadDescription("主键")]
        public bool IsPrimarykey { get; set; }

        public object Value { get; set; }

        public int DecimalDigits { get; set; }
        [HeadDescription("保留小数位")]
        public int Scale { get; set; }

        public bool IsArray { get; set; }

        public bool IsJson { get; set; }

        public bool? IsUnsigned { get; set; }

        public int CreateTableFieldSort { get; set; }

        internal object SqlParameterDbType { get; set; }

        [HeadDescription("描述")]
        public string ColumnDescription { get; set; }
    }
}
