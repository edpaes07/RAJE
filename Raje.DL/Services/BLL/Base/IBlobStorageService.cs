using System.IO;
using System.Threading.Tasks;

namespace Raje.DL.Services.BLL.Base
{
    public interface IBlobStorageService : IDependencyInjectionService
    {
        Task<(bool, string)> SaveImageToStorage(string filename, string folder, byte[] imageBuffer = null, Stream stream = null, string oldFileName = null);
    }
}
