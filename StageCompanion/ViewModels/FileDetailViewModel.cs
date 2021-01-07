using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using StageCompanion.Repositories.Interfaces;
using Xamarin.Forms;

namespace StageCompanion.ViewModels
{
    [QueryProperty(nameof(FileId), nameof(FileId))]
    public class FileDetailViewModel : BaseViewModel
    {
        private readonly IFileRepository _fileRepository = DependencyService.Get<IFileRepository>();

        public string Name { get; set; }
        public string FileId { get; set; }
        public string Content { get; set; }
        public string FontSize { get; set; }
        public Color FontColor { get; set; }
        public ImageSource FileSource { get; set; }

        public async Task LoadFile()
        {
            try
            {
                var file = await _fileRepository.GetAsync(FileId);
                if (file != null)
                {
                    FileId = file.Id.ToString();
                    Name = file.Name;
                    FontColor = Color.Default;
                    Content = file.Content;
                    if (file.Extension == "jpg" || file.Extension =="jpeg" || file.Extension =="png" || file.Extension =="gif")
                    {
                        FileSource = ImageSource.FromStream(FetchFileStream);
                    }
                }
                else
                {
                    Name = "File not found";
                    FontSize = "36";
                    FontColor = Color.Red;
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        public Stream FetchFileStream()
        {
            byte[] byteArray = Convert.FromBase64String(Content);
            MemoryStream stream = new MemoryStream(byteArray);
            return stream;
        }
    }
}
