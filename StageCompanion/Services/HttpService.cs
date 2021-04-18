using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using StageCompanion.Services.Interfaces;
using Xamarin.Forms;

namespace StageCompanion.Services
{
    public class HttpService : IHttpService
    {
        private readonly string _hostname = "https://stage-companion.herokuapp.com/";
        //TODO: read hostname from a config file
        private readonly ISecureStorage _secureStorage;

        public HttpService(ISecureStorage secureStorage, string hostname = null)
        {
            _secureStorage = secureStorage;
            if (hostname != null)
            {
                _hostname = hostname;
            }
        }

        public HttpService()
        {
            _secureStorage = DependencyService.Get<ISecureStorage>();
        }

        public async Task<HttpResponseMessage> SendRequestAsync(HttpMethod method, string path, string json = "", bool useAuthorization = true)
        {
            using var client = new HttpClient
            {
                BaseAddress = new Uri(_hostname)
            };
            var request = new HttpRequestMessage(method, path);
            if (!string.IsNullOrEmpty(json))
            {
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                request.Headers.Add("Accept", "application/json");
            }
            if (useAuthorization)
            {
                string token = await _secureStorage.GetAsync("token");
                request.Headers.Add("Authorization", $"Bearer {token}");
            }
            request.Headers.Add("User-Agent", "StageCompanion-Mobile");
            return await client.SendAsync(request);
        }
    }
}
