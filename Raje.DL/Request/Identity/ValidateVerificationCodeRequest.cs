using Raje.DL.Request.Base;

namespace Raje.DL.Request.Identity
{
    public class ValidateVerificationCodeRequest : BaseResetPasswordRequest
    {
        public int VerificationCode { get; set; }
    }
}
