using Raje.DL.Request.Admin.Base;

namespace Raje.DL.Request.Admin
{
    public class MediaSearchRequest : BaseSearchRequest
    {
        public string FileName { get; set; }

        public string Folder { get; set; }
    }
}
