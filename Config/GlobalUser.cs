using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Common.Config
{
    public class GlobalUser
    {
        /// <summary>
        /// 是否超级管理员
        /// </summary>
        public static bool SuperAdmin { get; set; }

        /// <summary>
        /// 当前用户账号
        /// </summary>
        public static string UserAccount { get; set; }

        /// <summary>
        /// 当前用户Id
        /// </summary>
        public static long UserId { get; set; }
    }
}
