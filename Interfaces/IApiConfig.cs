using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace XT.Common.Interfaces
{
    public interface IApiConfig
    {
        event EventHandler AuthorizationFailedEvent;
        /// <summary>
        /// Api Url
        /// </summary>
        public string RemoteApiUrl { get; set; }
        /// <summary>
        /// 其它Url
        /// </summary>
        public string OtherUrl { get; set; }

        /// <summary>
        /// Token数据
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 刷新Token
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// 触发验证失败的事件
        /// </summary>
        public void AuthorizationFailedInvoke();
        /// <summary>
        /// 获取httpClient
        /// </summary>
        /// <returns></returns>
        public HttpClient CreateHttpClient();


    }
}
