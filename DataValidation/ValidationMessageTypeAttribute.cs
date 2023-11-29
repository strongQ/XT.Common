using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Common.DataValidation
{
    /// <summary>
    /// 验证消息类型特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum)]
    public sealed class ValidationMessageTypeAttribute : Attribute
    {
    }
}
