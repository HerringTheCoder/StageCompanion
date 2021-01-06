using System.Net.Http;
using System.Threading.Tasks;

namespace StageCompanion.Interfaces
{
    public interface IHttpService
    {
        Task<HttpResponseMessage> SendRequestAsync(HttpMethod method, string path, string json = "");
    }
}
