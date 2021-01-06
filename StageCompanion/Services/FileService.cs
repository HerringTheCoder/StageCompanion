using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using StageCompanion.Interfaces;
using StageCompanion.Repositories.Interfaces;
using Xamarin.Essentials;
using Xamarin.Forms;
using File = StageCompanion.Models.File;

namespace StageCompanion.Services
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;

        public FileService()
        {
            _fileRepository = DependencyService.Get<IFileRepository>();
        }

        public async Task SendFile(Stream stream, FileResult result)
        {
            var fileName = result.FileName.Split('.');
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            var imageArray = memoryStream.ToArray();
            var file = new File
            {
                Name = fileName.First(),
                Extension = fileName.Last(),
                FolderId = 1,
                Content = Convert.ToBase64String(imageArray)
            };
            await _fileRepository.AddAsync(file);
        }
    }
}
