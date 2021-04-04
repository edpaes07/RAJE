namespace Raje.DL.Request.Admin.Base
{
    public interface IBaseLoginRequest
    {
        string UserName { get; set; }

        string Password { get; set; }
    }
}