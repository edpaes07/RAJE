using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Raje.Models
{
    public class Avaliacao
    {
        public Guid Id { get; set; }

        [Required]
        public int Nota { get; set; }

        [Required]
        public String Comentario { get; set; }

        [Required]
        [Display(Name = "User")]
        public String UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public Guid LivroId { get; set; }
        [ForeignKey("LivroId")]
        public virtual Livro Livro { get; set; }

        public Guid FilmeId { get; set; }
        [ForeignKey("FilmeId")]
        public virtual Filme Filme { get; set; }

        public Guid SerieId { get; set; }
        [ForeignKey("SerieId")]
        public virtual Serie Serie { get; set; }
    }
}