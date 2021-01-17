using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PropertyChanged;

namespace StageCompanion.Models.Requests
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    [AddINotifyPropertyChangedInterface]
    public class InvitationRequest
    {
        public string Email { get; set; }
        public int BandId { get; set; }
        public string Role { get; set; }
    }
}
