using Raje.DL.Request.Admin.Base;

namespace Raje.DL.Request.Admin
{
    public class UserRequest : BaseRequest
    {
        public string FullName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public long UserRoleId { get; set; }
    }
}
