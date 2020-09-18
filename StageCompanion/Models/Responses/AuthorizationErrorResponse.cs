using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PropertyChanged;
using System.Collections.Generic;

namespace StageCompanion.Models.Responses
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    [AddINotifyPropertyChangedInterface]
    public class AuthorizationErrorResponse : Response
    {
        public List<string> Email { get; set; }
        public List<string> Password { get; set; }

        public AuthorizationErrorResponse()
        {
            ColorName = "Red";
        }
    }
}
