using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Common.DataValidation
{
    /// <summary>
    /// 跳过验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public sealed class NonValidationAttribute : Attribute
    {
    }
}
