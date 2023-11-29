using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Common.Models.Server
{
    /// <summary>
    /// 错误消息 // '{"status":400,"error":"BadRequest","message":"用户不存在","timestamp":"1669078403867","path":"/auth/applogin"}'
    /// </summary>
    public class HttpCodeResult
    {
        public int Status { get; set; }

        public string Error { get; set; }

        public string Message { get; set; }

        public string Timestamp { get; set; }

        public string Path { get; set; }
    }
}
