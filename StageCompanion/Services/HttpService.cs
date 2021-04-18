using StageCompanion.Interfaces;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StageCompanion.Services
{
    public class HttpService : IHttpService
    {
        public const string Url = "https://stage-companion.herokuapp.com/";
        //TODO: create a config file
        private string _token = "";
        private readonly ISecureStorage _secureStorage;

        public HttpService(ISecureStorage secureStorage)
        {
            _secureStorage = secureStorage;
        }

        public HttpService()
        {
            _secureStorage = DependencyService.Get<ISecureStorage>();
        }

        public async Task<HttpResponseMessage> SendRequestAsync(HttpMethod method, string path, string json = "", bool useAuthorization = true)
        {
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
            if (useAuthorization)
            {
                _token = await _secureStorage.GetAsync("token");
                request.Headers.Add("Authorization", $"Bearer {_token}");
            }
            request.Headers.Add("User-Agent", "StageCompanion-Mobile");
            return await client.SendAsync(request);
        }
    }
}
