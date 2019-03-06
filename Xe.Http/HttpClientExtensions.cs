using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Xe.Http
{
    public static class HttpClientExtensions
    {
        public static async Task<T> GetJson<T>(this HttpClient httpClient, string uri, Dictionary<string, string> query = null)
        {
            var strQuery = query?.Count > 0 ? "?" +
                string.Join("&", query.Select(x => $"{x.Key}={x.Value}")) : string.Empty;

            using (var response = await httpClient.GetAsync(uri + strQuery))
            {
                try
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(content);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception {e.Message} on request {uri}");
                    throw;
                }
            }
        }
    }
}
