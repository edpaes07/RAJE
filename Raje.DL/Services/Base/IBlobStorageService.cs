using Raje.DL.DB.Admin;
using System.IO;
using System.Threading.Tasks;

namespace Raje.DL.Services.BLL.Base
{
    public interface IBlobStorageService : IDependencyInjectionService
    {
        Task<(bool, string)> SaveImageToStorage(string filename, string folder, byte[] imageBuffer = null, Stream stream = null, string oldFileName = null);
       
        Task<Stream> DownloadMedia(string fileName, Media media);

        Task<Stream> DownloadFile(string fileName, Media media);
    }
}
