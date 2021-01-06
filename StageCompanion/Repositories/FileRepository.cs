using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Humanizer;
using Newtonsoft.Json;
using StageCompanion.Models;
using StageCompanion.Repositories.Interfaces;

namespace StageCompanion.Repositories
{
    public class FileRepository : BaseRepository<File>, IFileRepository
    {
        public async Task<List<File>> GetAllByFolderId(int folderId)
        {
            string path = $"/{nameof(File).Pluralize().Transform(To.LowerCase)}/{nameof(Folder).Transform(To.LowerCase)}/{folderId}";
        
            var response = await HttpService.SendRequestAsync(HttpMethod.Get, path);
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                var files = JsonConvert.DeserializeObject<List<File>>(responseContent);
                return files;
            }

            return new List<File>();
        }

        public override async Task<File> GetAsync(string fileId)
        {
            string path = $"/{nameof(File).Pluralize().Transform(To.LowerCase)}/download/{fileId}";

            var response = await HttpService.SendRequestAsync(HttpMethod.Get, path);
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                var file = JsonConvert.DeserializeObject<File>(responseContent);
                return file;
            }

            return null;
        }
    }
}
