using System.Threading.Tasks;
using StageCompanion.Services.Interfaces;
using Xamarin.Essentials;

namespace StageCompanion.Services
{
    public class Storage : ISecureStorage
    {
        public async Task<string> GetAsync(string key)
        {
            return await SecureStorage.GetAsync(key);
        }
    }
}
