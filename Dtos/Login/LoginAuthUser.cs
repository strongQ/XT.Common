using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Common.Dtos.Login
{
    /// <summary>
    /// 登录用户
    /// </summary>
    public class LoginAuthUser
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        ///// <summary>
        ///// 不启用Code
        ///// </summary>
        public bool IsNoCode { get; set; } = true;





    }
}
