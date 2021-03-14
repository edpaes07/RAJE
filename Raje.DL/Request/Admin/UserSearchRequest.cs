using Raje.DL.Request.Admin.Base;
using Raje.DL.Request.Base;

namespace Raje.DL.Request.Admin
{
    public class UserSearchRequest : BaseSearchRequest
    {
        public string UserName { get; set; }
    }
}