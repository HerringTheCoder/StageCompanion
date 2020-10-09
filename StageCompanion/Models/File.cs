using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace StageCompanion.Models
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class File
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int FolderId { get; set; }

        public Folder Folder { get; set; }

        public string Path { get; set; }

        public string Extension { get; set; }

        public string Content { get; set; }
    }
}
