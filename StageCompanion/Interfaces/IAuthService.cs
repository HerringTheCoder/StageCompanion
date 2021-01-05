using StageCompanion.Models;
using System.Threading.Tasks;

namespace StageCompanion.Interfaces
{
    public interface IAuthService
    {       
        Task<bool> Login(Credentials credentials);
        Task<string> Register(Credentials credentials);
        Task<bool> LoginFromStorage();
    }
}
