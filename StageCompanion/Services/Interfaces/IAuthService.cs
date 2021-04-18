using System.Threading.Tasks;
using StageCompanion.Models;

namespace StageCompanion.Services.Interfaces
{
    public interface IAuthService
    {       
        Task<bool> Login(Credentials credentials);
        Task<string> Register(Credentials credentials);
        Task<bool> LoginFromStorage();
    }
}
