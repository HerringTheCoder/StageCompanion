using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using StageCompanion.Interfaces;
using StageCompanion.Models.Requests;
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

        public async Task<bool> SendFile(int folderId, Stream stream, FileResult result)
        {
            var fileName = result.FileName.Split('.');
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            var imageArray = memoryStream.ToArray();
            var file = new File
            {
                Name = fileName.First(),
                FolderId = folderId,
                Extension = fileName.Last(),
                Content = Convert.ToBase64String(imageArray)
            };
            return await _fileRepository.AddAsync(file);
        }

        public async Task<bool> SendBandFile(int bandId, string userId, Stream stream, FileResult result)
        {
            var fileName = result.FileName.Split('.');
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            var imageArray = memoryStream.ToArray();
            var file = new BandFileRequest
            {
                Name = fileName.First(),
                UserId = userId,
                BandId= bandId,
                Extension = fileName.Last(),
                Content = Convert.ToBase64String(imageArray)
            };
            return await _fileRepository.AddBandFileAsync(file);
        }
    }
}
