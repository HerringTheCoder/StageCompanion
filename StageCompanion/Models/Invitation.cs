using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace StageCompanion.Models
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class Invitation
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int BandId { get; set; }

        public bool Accepted { get; set; }
    }
}
