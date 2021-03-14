namespace Raje.DL.Services.BLL.Identity
{
    public class JwtOptions
    {
        public string JwtKey { get; set; }

        public string JwtIssuer { get; set; }

        public int JwtExpireMinutes { get; set; }

    }
}
