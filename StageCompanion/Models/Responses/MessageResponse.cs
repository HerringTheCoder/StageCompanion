using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Drawing;

namespace StageCompanion.Models.Responses
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class MessageResponse : Response
    {       
        public MessageResponse()
        {
            ColorName = "Green";
        }
    }
}
