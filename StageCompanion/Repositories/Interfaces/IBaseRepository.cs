using System.Collections.Generic;
using System.Threading.Tasks;

namespace StageCompanion.Repositories.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<bool> AddAsync(T item);
        Task<bool> UpdateAsync(T item);
        Task<bool> DeleteAsync(string id);
        Task<T> GetAsync(string id);
        Task<List<T>> GetAllAsync();
    }
}
