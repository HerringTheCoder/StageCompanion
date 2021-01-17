using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Humanizer;
using Newtonsoft.Json;
using StageCompanion.Models;
using StageCompanion.Repositories.Interfaces;

namespace StageCompanion.Repositories
{
    public class BandRepository : BaseRepository<Band>, IBandRepository
    {
        public virtual async Task<List<Band>> GetAllRegisteredAsync()
        {
            string path = "/" + nameof(Band).Pluralize().Transform(To.LowerCase) + "/filter/membership";
            var response = await HttpService.SendRequestAsync(HttpMethod.Get, path);
            string responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                List<Band> items = JsonConvert.DeserializeObject<List<Band>>(responseContent);
                return await Task.FromResult(items);
            }

            return await Task.FromResult(new List<Band>());
        }

        public async Task<bool> LeaveBandAsync(string bandId, string userId)      
        {
            string path = $"bands/{bandId}/leave/{userId}";
            var response = await HttpService.SendRequestAsync(HttpMethod.Get, path);
            string message = await response.Content.ReadAsStringAsync();

            return await Task.FromResult(response.IsSuccessStatusCode);
        
        }
    }
}
