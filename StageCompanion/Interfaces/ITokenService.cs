using StageCompanion.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StageCompanion.Interfaces
{
    public interface ITokenService
    {
        public JwtSecurityToken DecryptJwtToken(string token);
        public Task<string> GetTokenValueAsync(HttpResponseMessage response);
        public User GetUserFromToken(string token);
        public Task EncryptUserClaimsAsync(string token, Credentials credentials, JwtSecurityToken jwt);
        public Task<bool> ValidateToken();
    }
}
