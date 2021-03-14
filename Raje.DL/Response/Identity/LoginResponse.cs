namespace Raje.DL.Response.Identity
{
    public class LoginResponse
    {
        public string Token { get; set; }

        public bool FirstAccess { get; set; }

        public string RefreshToken { get; set; }
    }
}
