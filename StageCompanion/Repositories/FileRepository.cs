using Newtonsoft.Json;
using StageCompanion.Interfaces;
using StageCompanion.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StageCompanion.Repositories
{
    public class FileRepository : IDataStore<File>
    {
        private IHttpService HttpService => DependencyService.Get<IHttpService>();

        public async Task<bool> AddItemAsync(File file)
        {
            string json = JsonConvert.SerializeObject(file);
            await HttpService.SendRequestAsync(HttpMethod.Post, "/files", json);
            return await Task.FromResult(true);
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<File> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<File>> GetItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItemAsync(File item)
        {
            throw new NotImplementedException();
        }
    }
}
