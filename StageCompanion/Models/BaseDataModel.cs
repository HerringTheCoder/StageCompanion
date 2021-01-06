using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace StageCompanion.Models
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class BaseDataModel
    {
    }
}
