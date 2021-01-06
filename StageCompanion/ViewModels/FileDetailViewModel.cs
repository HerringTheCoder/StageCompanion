using System;
using System.Diagnostics;
using System.IO;
using System.Text;
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
        public ImageSource FileSource { get; set; }

        public async void LoadFile()
        {
            try
            {
                var file = await _fileRepository.GetAsync(FileId);
                if (file != null)
                {
                    FileId = file.Id.ToString();
                    Name = file.Name;
                    if (file.Extension == "jpg" || file.Extension =="jpeg" || file.Extension =="png" || file.Extension =="gif")
                    {
                        byte[] byteArray = Convert.FromBase64String(file.Content);
                        MemoryStream stream = new MemoryStream(byteArray);
                        FileSource = ImageSource.FromStream(() => stream);
                    }
                }
                else
                {
                    Name = "File not found";
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
