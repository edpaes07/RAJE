using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Raje.Models
{
    public class Amigo
    {
        public Guid Id { get; set; }

        public String UserId { get; set; }

        [Required]
        [Display(Name = "Amigo")]
        public String AmigoId { get; set; }
        [ForeignKey("AmigoId")]
        public virtual ApplicationUser Friend { get; set; }

        public bool Ativo { get; set; }
    }
}