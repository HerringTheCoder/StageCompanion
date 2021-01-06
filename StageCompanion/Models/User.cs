using System.Collections.Generic;

namespace StageCompanion.Models
{
    public class User : BaseDataModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Folder> Folders { get; set; }
        public List<Invitation> Invitations { get; set; }
    }
}
