using Raje.DL.DB.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Raje.DL.DB.Admin
{
    public class Contents : EntityAuditBase
    {
        [Required]
        [MaxLength(6)]
        public string Type { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(100)]
        public string Author { get; set; }

        [MaxLength(100)]
        public string Publisher { get; set; }

        [MaxLength(100)]
        public string Director { get; set; }

        [MaxLength(200)]
        public string MainCast { get; set; }

        [Required]
        [MaxLength(100)]
        public string Country { get; set; }

        [Required]
        [MaxLength(4)]
        public int ReleaseYear { get; set; }

        [MaxLength(2)]
        public int NumberSeasons { get; set; }

        [Required]
        public string Synopsis { get; set; }

        public bool IsValid { get; set; }

        [ForeignKey("Media")]
        public long? MediaId { get; set; }

        public virtual Media Media { get; set; }

        public virtual IEnumerable<Assessment> Assessment { get; set; }
    }
}
