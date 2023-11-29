using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace XT.Common.Models.Action
{
    public class ActionModel<T>
    {
        public bool IsSuccess { get; set; }
        public string ErrorMsg { get; set; }

        public string StackTrace { get; set; }
        public T Data { get; set; }
    }
}
