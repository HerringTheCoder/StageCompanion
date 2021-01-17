using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PropertyChanged;

namespace StageCompanion.Models
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    [AddINotifyPropertyChangedInterface]
    public class Credentials
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public string Name { get; set; }
    }
}
