using Raje.DL.DB.Base;
using System.ComponentModel.DataAnnotations;

namespace Raje.DL.DB.Admin
{
    public class State : EntityBase
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MaxLength(2)]
        public string Abbreviation { get; set; }

    }
}
