using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace StageCompanion.Services.Interfaces
{
    public interface IFileService
    {
        Task<bool> SendFile(int folderId, Stream stream, FileResult result);
        Task<bool> SendBandFile(int bandId, string userId, Stream stream, FileResult result);
    }
}
