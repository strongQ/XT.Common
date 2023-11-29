using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Common.Attributes
{
    public class OrderTableAttribute : Attribute
    {
        public int Order { get; set; } = 999;
    }
}
