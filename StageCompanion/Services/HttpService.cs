using StageCompanion.Interfaces;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StageCompanion.Services
{
    public class HttpService : IHttpService
    {
        public const string Url = "http://10.0.2.2:8000";
        public string Token = "";

        public async Task<HttpResponseMessage> SendRequestAsync(HttpMethod method, string path, string json = "")
        {
            using var client = new HttpClient
            {
                BaseAddress = new Uri(Url)
            };
            client.DefaultRequestHeaders.Add("Authentication", string.Format("Bearer {0}", Token));
            var request = new HttpRequestMessage(method, path);
            if (json != null)
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");                
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "StageCompanion-Mobile");
            return await client.SendAsync(request);
        }
    }
}
