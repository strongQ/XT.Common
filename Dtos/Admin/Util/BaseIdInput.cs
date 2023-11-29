﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using XT.Common.DataValidation;

namespace XT.Common.Dtos.Admin.Util
{
    // <summary>
    /// 主键Id输入参数
    /// </summary>
    public class BaseIdInput : BaseRender
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Required(ErrorMessage = "Id不能为空")]
        [DataValidation(ValidationTypes.Numeric)]
        public virtual long Id { get; set; }


    }
    /// <summary>
    /// 模板动态处理
    /// </summary>
    public class BaseRender
    {
        /// <summary>
        /// 是否被模板处理
        /// </summary>
        public bool IsRender { get; set; }
    }
}
