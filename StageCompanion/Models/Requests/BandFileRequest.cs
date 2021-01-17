using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PropertyChanged;

namespace StageCompanion.Models.Requests
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    [AddINotifyPropertyChangedInterface]
    public class BandFileRequest 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BandId { get; set; }
        public string UserId { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
        public string Content { get; set; }
    }
}
