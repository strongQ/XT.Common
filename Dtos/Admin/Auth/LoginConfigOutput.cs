using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Common.Dtos.Admin.Auth
{
    /// <summary>
    /// 登录配置
    /// </summary>
    public class LoginConfigOutput
    {
        /// <summary>
        /// 登录二次验证
        /// </summary>
        public bool SecondVerEnabled { get; set; }
        /// <summary>
        /// 图形验证码
        /// </summary>
        public bool CaptchaEnabled { get; set; }
        /// <summary>
        /// 水印
        /// </summary>
        public bool WatermarkEnabled { get; set; }
    }
}
