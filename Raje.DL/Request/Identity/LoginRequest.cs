using Raje.DL.Request.Admin.Base;

namespace Raje.DL.Request.Identity
{
    public class LoginRequest : BaseLoginRequest
    {
        public bool IsFirstAccessNeeded { get; set; } = true;
    }
}
