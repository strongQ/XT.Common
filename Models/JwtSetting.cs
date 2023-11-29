using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Common.Models
{
    public class JwtSetting
    {
        /// <summary>
        /// 颁发者
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 接收者
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 令牌密码
        /// </summary>
        public string SecurityKey { get; set; }

        /// <summary>
        ///  过期时间
        /// </summary>
        public long ExpireSeconds { get; set; }
    }
}
