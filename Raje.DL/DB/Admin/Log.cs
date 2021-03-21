using Raje.DL.DB.Base;
using System.ComponentModel.DataAnnotations;

namespace Raje.DL.DB.Admin
{
    public class Log : EntityAuditBase
    {

        public bool Input { get; set; }

        [Required]
        public short Code { get; set; }

        [Required]
        [MaxLength(200)]
        public string Api { get; set; }

        [MaxLength(2048)]
        public string UrlQuery { get; set; }

        [MaxLength(200)]
        public string Method { get; set; }

        public string Request { get; set; }

        public string Response { get; set; }

    }
}
