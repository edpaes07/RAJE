using Raje.DL.Request.Admin.Base;
using Microsoft.AspNetCore.Http;

namespace Raje.DL.Request.Admin
{
    public class MediaRequest : BaseRequest
    {
        public IFormFile Image { get; set; }

        public string Folder { get; set; }
    }
}
