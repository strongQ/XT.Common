using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Common.Dtos.Admin.Util
{
    public class BaseDto : BaseIdInput
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTime? UpdateTime { get; set; }


    }
}
