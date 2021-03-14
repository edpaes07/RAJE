using Raje.DL.Request.Base;

namespace Raje.DL.Request.Identity
{
    public class ResetPasswordRequest : BaseResetPasswordRequest
    {
        public string Password { get; set; }

        public int VerificationCode { get; set; }
    }
}
