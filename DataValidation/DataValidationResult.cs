﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace XT.Common.DataValidation
{
    /// <summary>
    /// 数据验证结果
    /// </summary>
    public sealed class DataValidationResult
    {
        /// <summary>
        /// 验证状态
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// 验证结果
        /// </summary>
        public ICollection<ValidationResult> ValidationResults { get; set; }

        /// <summary>
        /// 成员或值
        /// </summary>
        public object MemberOrValue { get; set; }
    }
}
