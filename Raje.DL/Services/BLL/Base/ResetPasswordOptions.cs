namespace Raje.DL.Services.BLL.Base
{
    public class ResetPasswordOptions
    {
        public string Url { get; set; }

        public string TokenKey { get; set; }

        public int ExpirationMinutes { get; set; }
    }
}
