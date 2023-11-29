using System;
using System.Collections.Generic;
using System.Text;
using XT.Common.Enums;

namespace XT.Common.Models.SignalR
{
    public class CloseModel
    {
        public SignalRFailEnum Type { get; set; }

        public string Reason { get; set; }
    }
}
