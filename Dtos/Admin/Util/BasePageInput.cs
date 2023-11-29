using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml;
using XT.Common.DataValidation;

namespace XT.Common.Dtos.Admin.Util
{
    /// <summary>
    /// 全局分页查询输入参数
    /// </summary>
    public class BasePageInput
    {
        /// <summary>
        /// 关键字
        /// </summary>
        [Description("关键字")]
        public virtual string SearchKey { get; set; }
        /// <summary>
        /// 当前页码
        /// </summary>
        [DataValidation(ValidationTypes.Numeric)]
        public virtual int Page { get; set; } = 1;

        /// <summary>
        /// 页码容量
        /// </summary>
        [Range(0, 100, ErrorMessage = "页码容量超过最大限制")]
        [DataValidation(ValidationTypes.Numeric)]
        public virtual int PageSize { get; set; } = 20;

        /// <summary>
        /// 排序字段
        /// </summary>
        public virtual string Field { get; set; }

        /// <summary>
        /// 排序方向
        /// </summary>
        public virtual string Order { get; set; }

        /// <summary>
        /// 降序排序
        /// </summary>
        public virtual string DescStr { get; set; } = "descend";

        /// <summary>
        /// 排序列
        /// </summary>
        public List<string> SortFields { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalElements { get; set; }


    }
}
