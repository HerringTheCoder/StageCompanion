using System.Collections.Generic;
using System.Threading.Tasks;
using StageCompanion.Models;

namespace StageCompanion.Repositories.Interfaces
{
    public interface IFileRepository : IBaseRepository<File>
    {
        Task<List<File>> GetAllByFolderId(int folderId);
    }
}
