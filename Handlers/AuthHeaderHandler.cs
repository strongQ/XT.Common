using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XT.Common.Extensions;
using XT.Common.Interfaces;

namespace XT.Common.Handlers
{
    

    public class AuthHeaderHandler : DelegatingHandler
    {
        private IApiConfig _apiConfig;
        private const string accessTokenKey = "access-token";
        private const string refreshAccessTokenKey = $"x-{accessTokenKey}";
    
        public AuthHeaderHandler(IApiConfig apiConfig)
        {
           _apiConfig = apiConfig;
        }

       
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // =======================================================
            //  请求拦截逻辑 (代码在 base.SendAsync 调用之前)
            // =======================================================
            var accessToken = _apiConfig.Token;

            if (!string.IsNullOrEmpty(accessToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var expirationDate = JwtDecoder.GetExpirationDate(accessToken);
                if (DateTime.UtcNow >= expirationDate.AddMinutes(-1))
                {
                    Console.WriteLine("--> Handler: Access token expired, attaching refresh token.");
                    var refreshToken = _apiConfig.RefreshToken;
                    if (!string.IsNullOrEmpty(refreshToken))
                    {
                        request.Headers.Add("X-Authorization", $"Bearer {refreshToken}");
                    }
                }
            }

            // 发送请求到下一个 Handler，并等待服务器响应
            var response = await base.SendAsync(request, cancellationToken);

            // =======================================================
            //  响应拦截逻辑 (代码在 base.SendAsync 调用之后)
            // =======================================================

            // 检查响应头中是否有后端返回的新 token
            // 假设后端通过 'X-Access-Token' 和 'X-Refresh-Token' 头返回新 token
            if (response.Headers.TryGetValues(accessTokenKey, out var newAccessTokens))
            {
                string? newAccessToken = newAccessTokens.FirstOrDefault();
                
                string? newRefreshToken = null;

                if (response.Headers.TryGetValues(refreshAccessTokenKey, out var newRefreshTokens))
                {
                    newRefreshToken = newRefreshTokens.FirstOrDefault();
                 
                }

                if (!string.IsNullOrEmpty(newAccessToken))
                {
                    _apiConfig.Token = newAccessToken;
                    _apiConfig.RefreshToken = newRefreshToken;
                }


            }

            // 将原始响应返回给调用方
            return response;
        }
    
    }
}
