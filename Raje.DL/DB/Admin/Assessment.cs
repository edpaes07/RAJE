using Raje.DL.DB.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Raje.DL.DB.Admin
{
    public class Assessment : EntityAuditBase
    {
        [Required]
        [MaxLength(2)]
        public int Grade { get; set; }

        [Required]
        [MaxLength(1024)]
        public string Commentary { get; set; }

        [ForeignKey("User")]
        public long UserId { get; set; }

        public User User { get; set; }

        [ForeignKey("Contents")]
        public long ContentsId { get; set; }

        public Contents Contents { get; set; }
    }
}
