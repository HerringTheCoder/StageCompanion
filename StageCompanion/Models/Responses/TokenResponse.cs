using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace StageCompanion.Models.Responses
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class TokenResponse
    {
        public string Token { get; set; }
    }
}