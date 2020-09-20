using Newtonsoft.Json;
using StageCompanion.Interfaces;
using StageCompanion.Models;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using System;
using System.Net;
using StageCompanion.Models.Responses;
using System.IdentityModel.Tokens.Jwt;
using Xamarin.Essentials;

namespace StageCompanion.Services
{
    public class AuthService : IAuthService
    {
        private IHttpService HttpService => DependencyService.Get<IHttpService>();

        public async Task<string> GetValidationErrors(HttpResponseMessage response)
        {
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<bool> Login(Credentials credentials)
        {
            string json = JsonConvert.SerializeObject(credentials);
            var response = await HttpService.SendRequestAsync(HttpMethod.Post, "/auth/login", json);
            if (response.IsSuccessStatusCode)
            {
                string token = await GetTokenValueAsync(response);
                var jwt = DecryptJwtToken(token);
                await EncryptUserDataAsync(token, credentials, jwt);
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
            var response = await HttpService.SendRequestAsync(HttpMethod.Post, "/auth/register", json);
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

        public async Task<bool> ValidateToken()
        {
            string expiredAt = await SecureStorage.GetAsync("expiredAt");
            if (!string.IsNullOrEmpty(expiredAt))
            {
                DateTime expiredAtTime = DateTime.Parse(expiredAt);
                if (expiredAtTime > DateTime.UtcNow)
                {
                    return true;
                }
                else
                {
                    return await RefreshToken();
                }
            }
            return false;
        }

        private async Task EncryptUserDataAsync(string token, Credentials credentials, JwtSecurityToken jwt)
        {
            await SecureStorage.SetAsync("token", token);
            await SecureStorage.SetAsync("password", credentials.Password);
            await SecureStorage.SetAsync("email", credentials.Password);
            DateTime expiredAt = DateTimeOffset.FromUnixTimeSeconds((long)jwt.Payload.Exp).UtcDateTime;
            await SecureStorage.SetAsync("expiredAt", expiredAt.ToString());            
        }       

        private JwtSecurityToken DecryptJwtToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            return jwt;
        }

        private async Task<string> GetTokenValueAsync(HttpResponseMessage response)
        {
            string responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TokenResponse>(responseContent).Token;
        }       

        private async Task<bool> RefreshToken()
        {
            var authService = DependencyService.Get<AuthService>();
            string email = await SecureStorage.GetAsync("email");
            string password = await SecureStorage.GetAsync("password");
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                var credentials = new Credentials
                {
                    Email = email,
                    Password = password
                };
                return await authService.Login(credentials);
            }
            return false;
        }
    }
}
