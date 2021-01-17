using StageCompanion.Interfaces;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using StageCompanion.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace StageCompanion.Services
{
    public class HttpService : IHttpService
    {
        public const string Url = "https://stage-companion.herokuapp.com/";
        private string _token = "";

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
                _token = await SecureStorage.GetAsync("token");
                request.Headers.Add("Authorization", $"Bearer {_token}");
            }
            request.Headers.Add("User-Agent", "StageCompanion-Mobile");
            return await client.SendAsync(request);
        }
    }
}
