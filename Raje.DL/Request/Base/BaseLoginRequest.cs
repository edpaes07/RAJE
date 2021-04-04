namespace Raje.DL.Request.Admin.Base
{
    public class BaseLoginRequest : IBaseLoginRequest
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
