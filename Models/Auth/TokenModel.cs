using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Common.Models.Auth
{
    public class TokenModel
    {
        public string AccessToken { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public int ExpiresIn { get; set; }
    }
}
