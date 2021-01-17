namespace StageCompanion.Models
{
    public class Folder : BaseDataModel
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public User Owner { get; set; }
        public int? BandId { get; set; }
        public Band Band { get; set; }
        public string Name { get; set; }
    }
}
