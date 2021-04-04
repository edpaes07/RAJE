namespace Raje.DL.Request.Admin.Base
{
    public class BaseRefreshTokenRequest : IBaseRefreshTokenRequest
    {
        public string UserName { get; set; }

        public string RefreshToken { get; set; }
    }
}
