using Raje.DL.Request.Admin.Base;

namespace Raje.DL.Request.Identity
{
    public class ResetPasswordEmailRequest : BaseResetPasswordRequest
    {
        public string Email { get; set; }
    }
}
