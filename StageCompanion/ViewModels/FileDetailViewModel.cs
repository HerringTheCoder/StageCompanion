using System;
using System.Diagnostics;
using StageCompanion.Repositories.Interfaces;
using Xamarin.Forms;

namespace StageCompanion.ViewModels
{
    [QueryProperty(nameof(FileId), nameof(FileId))]
    public class FileDetailViewModel : BaseViewModel
    {
        private readonly IFileRepository _fileRepository = DependencyService.Get<IFileRepository>();

        public string Name { get; set; }
        public string Content { get; set; }
        public string FileId { get; set; }

        public async void LoadFile()
        {
            try
            {
                var file = await _fileRepository.GetAsync(FileId);
                if (file != null)
                {
                    FileId = file.Id.ToString();
                    Name = file.Name;
                    Content = file.Content;
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
