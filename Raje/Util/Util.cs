using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Raje.Util
{
    public static class Util
    {
        public static bool UploadArquivo(IFormFile arquivo, string imgPrefixo)
        {
            if (arquivo.Length <= 0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imgPrefixo + arquivo.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                arquivo.CopyTo(stream);
            }

            return true;
        }
    }
}
