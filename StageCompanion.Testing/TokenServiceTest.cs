
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using StageCompanion.Models;
using StageCompanion.Services;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Xunit;

namespace StageCompanion.Testing
{
    public class TokenServiceTest
    {
        [Fact]
        public void TestIfTokenIsDecryptedProperly()
        {
            //Prepare
            var tokenService = new TokenService();
            var expectedUser = new User
            {
                Id = "TestGuid",
                Name = "testName",
                Email = "test@example.com"
            };
            string tokenMock = CreateTestToken(expectedUser);

            //Test
            JwtSecurityToken decryptedToken = tokenService.DecryptJwtToken(tokenMock);
            User actualUser = JsonConvert.DeserializeObject<User>(decryptedToken.Subject);

            //Assert
            Assert.Equal(expectedUser.Email, actualUser.Email);
            Assert.Equal(expectedUser.Name, actualUser.Name);
            Assert.Equal(expectedUser.Id, actualUser.Id);
        }

        private string CreateTestToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("secret16byteskey");

            var claims = new Dictionary<string, object>();
            claims.Add(JwtRegisteredClaimNames.Sub, user);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Claims = claims,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


    }
}
