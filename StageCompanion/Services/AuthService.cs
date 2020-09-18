using Newtonsoft.Json;
using StageCompanion.Interfaces;
using StageCompanion.Models;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using System;
using System.Net;
using System.Collections.Generic;
using StageCompanion.Models.Responses;
using System.IdentityModel.Tokens.Jwt;

namespace StageCompanion.Services
{
    public class AuthService : IAuthService
    {
        private IHttpService HttpService => DependencyService.Get<IHttpService>();

        public async Task<string> GetValidationErrors(HttpResponseMessage response)
        {
            return await response.Content.ReadAsStringAsync();
        }

        public async Task Login(Credentials credentials)
        {
            string json = JsonConvert.SerializeObject(credentials);
            var response = await HttpService.SendRequestAsync(HttpMethod.Post, "/auth/login", json);
            if (response.IsSuccessStatusCode)
            {
                //Write-in user's token and stuff and return
            }
            if (response.StatusCode == HttpStatusCode.BadRequest || response.StatusCode == (HttpStatusCode)422)
                throw new Exception("Wrong input format");

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

            if (!response.IsSuccessStatusCode)
            {
                var errors = JsonConvert.DeserializeObject<AuthorizationErrorResponse>(content);
                string emailErrors = errors.Email != null ? string.Join($"\n", errors.Email.ToArray()) : null;
                string passwordErrors = errors.Password != null ? string.Join($"\n", errors.Password.ToArray()) : null;
                throw new Exception(emailErrors + passwordErrors);
            }

            var messageResponse = JsonConvert.DeserializeObject<MessageResponse>(content);
            return messageResponse.Message;
        }

        public User GetUser(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var user = JsonConvert.DeserializeObject<User>(jwt.Subject);
            return user;
        }

        private async Task<string> GetTokenValue(HttpResponseMessage response)
        {
            string responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TokenResponse>(responseContent).Token;
        }
    }
}
