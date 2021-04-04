using Raje.DL.Request.Admin.Base;

namespace Raje.DL.Request.Identity
{
    public class ResetPasswordRequest : BaseResetPasswordRequest
    {
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Token { get; set; }
    }
}
