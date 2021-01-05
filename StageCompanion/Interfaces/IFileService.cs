using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace StageCompanion.Interfaces
{
    public interface IFileService
    {
        Task SendFile(Stream stream, FileResult result);
    }
}
