namespace StageCompanion.Models
{
    public class BandUser : BaseDataModel
    {
        public string UserId { get; set; }
        public int BandId { get; set; }
        public string Role { get; set; }
    }
}
