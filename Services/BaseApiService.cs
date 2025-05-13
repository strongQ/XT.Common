using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using XT.Common.Interfaces;

namespace XT.Common.Services
{
    public abstract class BaseApiService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IApiConfig _apiConfig;
        public BaseApiService(IHttpClientFactory httpClientFactory, IApiConfig apiConfig)
        {
            _clientFactory = httpClientFactory;
            _apiConfig = apiConfig;
        }
        public HttpClient CreateHttpClient()
        {
            var client = _clientFactory.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(30);
            if (!string.IsNullOrEmpty(_apiConfig.Token))
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _apiConfig.Token);
            client.BaseAddress = new Uri(_apiConfig.RemoteApiUrl);
            return client;
        }
    }
}
