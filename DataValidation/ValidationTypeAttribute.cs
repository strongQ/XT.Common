using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Common.DataValidation
{

    /// <summary>
    /// 验证类型特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum)]
    public sealed class ValidationTypeAttribute : Attribute
    {
    }
}
