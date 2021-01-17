using System.Threading.Tasks;
using StageCompanion.Models;
using StageCompanion.Models.Requests;

namespace StageCompanion.Repositories.Interfaces
{
    public interface IInvitationRepository : IBaseRepository<Invitation>
    {
        Task<bool> RequestNewAsync(InvitationRequest item);
        Task<bool> AcceptInvitationAsync(Invitation invitation);
    }
}
