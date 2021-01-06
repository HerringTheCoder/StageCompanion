using System.Collections.Generic;

namespace StageCompanion.Models
{
    public class Band : BaseDataModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LeaderId { get; set; }
        public User Leader { get; set; }
        public List<BandUser> BandUsers { get; set; }
    }
}
