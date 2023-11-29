using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace XT.Common.Attributes
{
    /// <summary>
    /// Head描述
    /// </summary>
    public class HeadDescriptionAttribute : Attribute
    {
        public HeadDescriptionAttribute(string name)
        {
            Name = name;
        }
        public string Name { get; set; }

    }
}
