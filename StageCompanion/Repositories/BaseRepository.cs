using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Humanizer;
using Newtonsoft.Json;
using StageCompanion.Interfaces;
using StageCompanion.Models;
using StageCompanion.Repositories.Interfaces;
using Xamarin.Forms;

namespace StageCompanion.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseDataModel, new()
    {
        protected IHttpService HttpService => DependencyService.Get<IHttpService>();

        public virtual async Task<bool> AddAsync(T item)
        {
            string path = "/" + typeof(T).Name.Pluralize().Transform(To.LowerCase);
            string json = JsonConvert.SerializeObject(item);
            var response = await HttpService.SendRequestAsync(HttpMethod.Post, path, json);
            string message = await response.Content.ReadAsStringAsync();

            return await Task.FromResult(response.IsSuccessStatusCode);
        }

        public virtual async Task<bool> UpdateAsync(T item)
        {
            string path = "/" + typeof(T).Name.Pluralize().Transform(To.LowerCase);
            string json = JsonConvert.SerializeObject(item);
            var response = await HttpService.SendRequestAsync(HttpMethod.Put, path, json);
            string message = await response.Content.ReadAsStringAsync();

            return await Task.FromResult(response.IsSuccessStatusCode);
        }

        public virtual async Task<bool> DeleteAsync(string id)
        {
            string path = "/" + typeof(T).Name.Pluralize().Transform(To.LowerCase) + $"/{id}";
            var response = await HttpService.SendRequestAsync(HttpMethod.Delete, path);
            string message = await response.Content.ReadAsStringAsync();

            return await Task.FromResult(response.IsSuccessStatusCode);
        }

        public virtual async Task<T> GetAsync(string id)
        {
            string path = "/" + typeof(T).Name.Pluralize().Transform(To.LowerCase) + $"/{id}";
            var response = await HttpService.SendRequestAsync(HttpMethod.Get, path);
            string responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                T item = JsonConvert.DeserializeObject<T>(responseContent);
                return await Task.FromResult(item);
            }

            return await Task.FromResult(new T());
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            string path = "/" + typeof(T).Name.Pluralize().Transform(To.LowerCase);
            var response = await HttpService.SendRequestAsync(HttpMethod.Get, path);
            string responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                List<T> items = JsonConvert.DeserializeObject<List<T>>(responseContent);
                return await Task.FromResult(items);
            }

            return await Task.FromResult(new List<T>());
        }
    }
}
