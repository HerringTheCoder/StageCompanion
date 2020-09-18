using StageCompanion.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace StageCompanion.Interfaces
{
    public interface IAuthService
    {       
        Task Login(Credentials credentials);
        Task<string> Register(Credentials Credentials);     
    }
}
