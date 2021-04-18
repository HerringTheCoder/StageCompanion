using System.Threading.Tasks;

namespace StageCompanion.Services.Interfaces
{
    public interface ISecureStorage
    {
        Task<string> GetAsync(string key);
    }
}
