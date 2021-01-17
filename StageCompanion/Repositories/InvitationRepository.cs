using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StageCompanion.Models;
using StageCompanion.Models.Requests;
using StageCompanion.Repositories.Interfaces;

namespace StageCompanion.Repositories
{
    public class InvitationRepository : BaseRepository<Invitation>, IInvitationRepository
    {
        public virtual async Task<bool> RequestNewAsync(InvitationRequest item)
        {
            string path = $"/invitations";
            string json = JsonConvert.SerializeObject(item);
            var response = await HttpService.SendRequestAsync(HttpMethod.Post, path, json);
            string message = await response.Content.ReadAsStringAsync();

            return await Task.FromResult(response.IsSuccessStatusCode);
        }

        public virtual async Task<bool> DeclineInvitationAsync(Invitation invitation)
        {
            string path = $"/invitations/{invitation.Id}";
            var response = await HttpService.SendRequestAsync(HttpMethod.Delete, path);
            string message = await response.Content.ReadAsStringAsync();

            return await Task.FromResult(response.IsSuccessStatusCode);
        }


        public virtual async Task<bool> AcceptInvitationAsync(Invitation invitation)
        {
            string path = $"/invitations/accept";
            string json = JsonConvert.SerializeObject(invitation);
            var response = await HttpService.SendRequestAsync(HttpMethod.Post, path, json);
            string message = await response.Content.ReadAsStringAsync();

            return await Task.FromResult(response.IsSuccessStatusCode);
        }


    }
}
