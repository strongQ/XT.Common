using System;
using System.Collections.Generic;
using System.Text;
using XT.Common.Dtos.Admin.Util;

namespace XT.Common.Dtos.Admin.OnlineUser
{
    public class OnlineUserInput : BasePageInput
    {
        /// <summary>
        /// 账号名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }
    }
}
