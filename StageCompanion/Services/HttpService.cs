using StageCompanion.Interfaces;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace StageCompanion.Services
{
    public class HttpService : IHttpService
    {
        public const string Url = "http://10.0.2.2:8000";
        private string _token = "";

        public async Task<HttpResponseMessage> SendRequestAsync(HttpMethod method, string path, string json = "")
        {
            _token = await SecureStorage.GetAsync("token");
            using var client = new HttpClient
            {
                BaseAddress = new Uri(Url)
            };
            var request = new HttpRequestMessage(method, path);
            if (!string.IsNullOrEmpty(json))
            {
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                request.Headers.Add("Accept", "application/json");
            }
            request.Headers.Add("Authorization", $"Bearer {_token}");
            request.Headers.Add("User-Agent", "StageCompanion-Mobile");
            return await client.SendAsync(request);
        }
    }
}
