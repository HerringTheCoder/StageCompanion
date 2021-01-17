using System.Collections.Generic;
using System.Threading.Tasks;
using StageCompanion.Models;
using StageCompanion.Models.Requests;

namespace StageCompanion.Repositories.Interfaces
{
    public interface IFileRepository : IBaseRepository<File>
    {
        Task<List<File>> GetAllByFolderId(int folderId);
        Task<bool> AddBandFileAsync(BandFileRequest bandFileRequest);
    }
}
