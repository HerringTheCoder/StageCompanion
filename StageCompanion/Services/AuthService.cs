using Newtonsoft.Json;
using StageCompanion.Models;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using System;
using System.Net;
using StageCompanion.Models.Responses;
using StageCompanion.Services.Interfaces;
using Xamarin.Essentials;

namespace StageCompanion.Services
{
    public class AuthService : IAuthService
    {
        private IHttpService HttpService => DependencyService.Get<IHttpService>();
        private ITokenService TokenService => DependencyService.Get<ITokenService>();

        public async Task<bool> Login(Credentials credentials)
        {
            string json = JsonConvert.SerializeObject(credentials);
            var response = await HttpService.SendRequestAsync(HttpMethod.Post, "/auth/login", json, false);
            if (response.IsSuccessStatusCode)
            {
                string token = await TokenService.GetTokenValueAsync(response);
                var jwt = TokenService.DecryptJwtToken(token);
                await TokenService.EncryptUserClaimsAsync(token, credentials, jwt);
                App.CurrentUser = JsonConvert.DeserializeObject<User>(jwt.Subject);
                return true;
            }
            if (response.StatusCode == HttpStatusCode.BadRequest || response.StatusCode == (HttpStatusCode)422)
            {
                string content = await response.Content.ReadAsStringAsync();
                var errors = JsonConvert.DeserializeObject<AuthorizationErrorResponse>(content);
                string emailErrors = errors.Email != null ? string.Join($"\n", errors.Email.ToArray()) : null;
                string passwordErrors = errors.Password != null ? string.Join($"\n", errors.Password.ToArray()) : null;
                throw new Exception(emailErrors + passwordErrors);
            }
            throw new Exception("These credentials do not match our records");
        }

        public async Task<string> Register(Credentials credentials)
        {
            if (credentials.PasswordConfirmation != credentials.Password)
            {
                throw new Exception("Provided passwords do not match!");
            }
            string json = JsonConvert.SerializeObject(credentials);
            var response = await HttpService.SendRequestAsync(HttpMethod.Post, "/auth/register", json, false);
            string content = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.BadRequest || response.StatusCode == (HttpStatusCode)422)
            {
                var errors = JsonConvert.DeserializeObject<AuthorizationErrorResponse>(content);
                string emailErrors = errors.Email != null ? string.Join($"\n", errors.Email.ToArray()) : null;
                string passwordErrors = errors.Password != null ? string.Join($"\n", errors.Password.ToArray()) : null;
                throw new Exception(emailErrors + passwordErrors);
            }

            var messageResponse = JsonConvert.DeserializeObject<MessageResponse>(content);
            return messageResponse.Message;
        }

        public async Task<bool> LoginFromStorage()
        {            
            string email = await SecureStorage.GetAsync("email");
            string password = await SecureStorage.GetAsync("password");
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                var credentials = new Credentials
                {
                    Email = email,
                    Password = password
                };
                return await Login(credentials);
            }
            return false;
        }


    }
}
