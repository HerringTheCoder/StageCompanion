using System.Collections.Generic;
using System.Threading.Tasks;
using StageCompanion.Models;

namespace StageCompanion.Repositories.Interfaces
{
    public interface IBandRepository : IBaseRepository<Band>
    {
        Task<List<Band>> GetAllRegisteredAsync();
        Task<bool> LeaveBandAsync(string bandId, string userId);
    }
}
