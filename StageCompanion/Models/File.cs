using Xamarin.Forms;

namespace StageCompanion.Models
{
    public class File : BaseDataModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FolderId { get; set; }
        public Folder Folder { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
        public string Content { get; set; }
        public ImageSource FileIcon { get; set; }
    }
}
