using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using XT.Common.Interfaces;

namespace XT.Common.Services
{
    public class ApiConfigService : IApiConfig
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ApiConfigService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;

        }
        public string RemoteApiUrl { get; set; }
        public string OtherUrl { get; set; }
        public string Token { get; set; }

        public event EventHandler AuthorizationFailedEvent;

        public void AuthorizationFailedInvoke()
        {
            AuthorizationFailedEvent?.Invoke(this, null);
        }

        public HttpClient CreateHttpClient()
        {
            var client = _httpClientFactory.CreateClient();
            if (!string.IsNullOrEmpty(Token))
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
            client.BaseAddress = new Uri(RemoteApiUrl);
            return client;
        }
    }
}
