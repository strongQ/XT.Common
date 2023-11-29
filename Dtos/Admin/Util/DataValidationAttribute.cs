﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace XT.Common.Dtos.Admin.Util
{
    /// <summary>
    /// 数据类型验证特性
    /// </summary>
    public class DataValidationAttribute : ValidationAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="validationPattern">验证逻辑</param>
        /// <param name="validationTypes"></param>
        public DataValidationAttribute(ValidationPattern validationPattern, params object[] validationTypes)
        {
            ValidationPattern = validationPattern;
            ValidationTypes = validationTypes;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="validationTypes"></param>
        public DataValidationAttribute(params object[] validationTypes)
        {
            ValidationPattern = ValidationPattern.AllOfThem;
            ValidationTypes = validationTypes;
        }

        /// <summary>
        /// 验证逻辑
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // 判断是否允许 空值
            if (AllowNullValue && value == null) return ValidationResult.Success;

            // 是否忽略空字符串
            if (AllowEmptyStrings && value is string && string.IsNullOrEmpty(value?.ToString())) return ValidationResult.Success;

            // 执行值验证




            // 验证成功
            return ValidationResult.Success;
        }

        /// <summary>
        /// 验证类型
        /// </summary>
        public object[] ValidationTypes { get; set; }

        /// <summary>
        /// 验证逻辑
        /// </summary>
        public ValidationPattern ValidationPattern { get; set; }

        /// <summary>
        ///是否允许空字符串
        /// </summary>
        public bool AllowEmptyStrings { get; set; } = false;

        /// <summary>
        /// 允许空值，有值才验证，默认 false
        /// </summary>
        public bool AllowNullValue { get; set; } = false;
    }
}
