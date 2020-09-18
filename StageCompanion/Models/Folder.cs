using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
namespace StageCompanion.Models
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class Folder
    {
        public int Id { get; set; }

        public string OwnerId { get; set; }

        public User Owner { get; set; }

        public string Name { get; set; }

    }
}
