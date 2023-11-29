using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Common.Dtos.Admin.Auth
{
    public class CaptchaOutput
    {
        public long Id { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Img { get; set; }
    }
}
