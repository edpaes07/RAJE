using Microsoft.AspNetCore.Http;
using Raje.DL.DB.Admin;
using Raje.DL.Request.Admin.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Raje.DL.Request.Admin
{
    public class ContentsRequest : BaseRequest
    {
        [Required]
        public string Type { get; set; }

        [Required]
        public string Title { get; set; }

        public string Author { get; set; }

        public string Publisher { get; set; }

        public string Director { get; set; }

        public string MainCast { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public int ReleaseYear { get; set; }

        public int NumberSeasons { get; set; }

        [Required]
        public string Synopsis { get; set; }

        public bool IsValid { get; set; }

        public virtual IFormFile Media { get; set; }

        public IEnumerable<Assessment> Assessment { get; set; }
    }
}
