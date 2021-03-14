namespace Raje.DL.Services.BLL.Base
{
    public class SMTPOptions
    {
        public string PrimaryDomain { get; set; }

        public int PrimaryPort { get; set; }

        public string UsernameEmail { get; set; }

        public string EnableSSL { get; set; }

        public string UsernamePassword { get; set; }

        public string From { get; set; }
    }
}
