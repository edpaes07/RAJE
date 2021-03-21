using Raje.DL.DB.Base;
using System.ComponentModel.DataAnnotations;

namespace Raje.DL.DB.Admin
{
    //TODO: Definir depois do identity
    public class UserRole : EntityBase
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public int SystemCode { get; set; }
    }
}
