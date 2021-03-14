using Raje.DL.Response.Base;
using Raje.DL.Services.DAL;

namespace Raje.DL.Response.Admin
{
    public class MediaSearchResponse : BaseResponse, IEntity
    {
        public string FileName { get; set; }

        public string FilePath { get; set; }

        public string Folder { get; set; }
    }
}
