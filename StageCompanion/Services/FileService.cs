﻿using System;
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
        private IBaseRepository<File> FileRepository => DependencyService.Get<IBaseRepository<File>>();

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
            await FileRepository.AddAsync(file);
        }
    }
}