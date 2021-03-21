using Raje.DL.DB.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Raje.DL.DB.Admin
{
    public class City : EntityBase
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [ForeignKey("State")]
        public long StateId { get; set; }

        public State State { get; set; }

    }
}
