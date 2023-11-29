using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Common.Models.Server
{
    public class AdminCodeResult<T>
    {
        /// <summary>
        /// http Code
        /// </summary>
        public int Code { get; set; }

        public bool Success { get; set; }
        /// <summary>
        /// success
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 额外信息
        /// </summary>
        public string Extras { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime? Time { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        public T Result { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }
    }
}
