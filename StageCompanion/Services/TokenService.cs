using Newtonsoft.Json;
using StageCompanion.Interfaces;
using StageCompanion.Models;
using StageCompanion.Models.Responses;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace StageCompanion.Services
{
    public class TokenService : ITokenService
    {
        private IAuthService AuthService => DependencyService.Get<IAuthService>();

        public JwtSecurityToken DecryptJwtToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            return jwt;
        }

        public async Task<string> GetTokenValueAsync(HttpResponseMessage response)
        {
            string responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TokenResponse>(responseContent).Token;
        }

        public User GetUserFromToken(string token)
        {
            var jwt = DecryptJwtToken(token);
            return JsonConvert.DeserializeObject<User>(jwt.Subject);
        }

        public async Task EncryptUserClaimsAsync(string token, Credentials credentials, JwtSecurityToken jwt)
        {           
            await SecureStorage.SetAsync("token", token);
            await SecureStorage.SetAsync("password", credentials.Password);
            await SecureStorage.SetAsync("email", credentials.Password);
            DateTime expiredAt = DateTimeOffset.FromUnixTimeSeconds((long)jwt.Payload.Exp).UtcDateTime;
            await SecureStorage.SetAsync("expiredAt", expiredAt.ToString());
        }

        public async Task<bool> ValidateToken()
        {
            string expiredAt = await SecureStorage.GetAsync("expiredAt");
            if (!string.IsNullOrEmpty(expiredAt))
            {
                DateTime expiredAtTime = DateTime.Parse(expiredAt);
                if (expiredAtTime > DateTime.UtcNow)
                    return true;
                else
                    return await AuthService.LoginFromStorage();
            }
            return false;
        }


    }
}
