using System.Threading.Tasks;

namespace StageCompanion.Interfaces
{
    public interface ISecureStorage
    {
        Task<string> GetAsync(string key);
    }
}
