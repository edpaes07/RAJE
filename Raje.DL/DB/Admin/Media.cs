using Raje.DL.DB.Base;
using System.ComponentModel.DataAnnotations;

namespace Raje.DL.DB.Admin
{
    public class Media : EntityAuditBase
    {
        [Required]
        [MaxLength(400)]
        public string FileName { get; set; }

        [Required]
        [MaxLength(1000)]
        public string FilePath { get; set; }

        [Required]
        [MaxLength(200)]
        public string Folder { get; set; }
    }
}
