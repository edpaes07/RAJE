using Raje.DL.Response.Base;

namespace Raje.DL.Response.Admin
{
    public class MediaResponse : BaseResponse
    {
        public string FileName { get; set; }

        public string FilePath { get; set; }

        public string Folder { get; set; }
    }
}
