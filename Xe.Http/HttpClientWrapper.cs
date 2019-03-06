using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Xe.Http
{
    public class HttpClientWrapper : IDisposable
    {
        private readonly HttpClient _httpClient;

        public string BaseApi { get; }

        public HttpClientWrapper(string baseApi)
        {
            BaseApi = baseApi;
            _httpClient = NewHttpClient();
        }

        public Task<T> GetJson<T>(string endpoint, Dictionary<string, string> query = null)
        {
            return _httpClient.GetJson<T>(BaseApi + endpoint, query);
        }

        private static HttpClient NewHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Xe.Http", "1.0"));
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
