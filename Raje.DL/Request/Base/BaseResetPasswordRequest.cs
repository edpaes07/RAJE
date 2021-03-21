namespace Raje.DL.Request.Admin.Base
{
    public class BaseResetPasswordRequest : IBaseResetPasswordRequest
    {
        public string UserName { get; set; }
    }
}
