using Raje.DL.Request.Admin.Base;

namespace Raje.DL.Request.Admin
{
    public class ContentsSearchRequest : BaseSearchRequest
    {
        public string Type { get; set; }

        public string Title { get; set; }
    }
}
