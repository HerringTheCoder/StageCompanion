using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PropertyChanged;

namespace StageCompanion.Models.Responses
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    [AddINotifyPropertyChangedInterface]
    public class Response
    {
        public string Message { get; set; }

        [JsonIgnore]
        public string ColorName { get; set; }           
    }
}
