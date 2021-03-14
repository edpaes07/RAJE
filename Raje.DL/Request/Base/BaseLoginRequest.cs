namespace Raje.DL.Request.Base
{
    public class BaseLoginRequest : IBaseLoginRequest
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }
}
