using System;
using System.ComponentModel.DataAnnotations;

namespace Raje.Models
{
    public class Filme
    {
        public Guid Id { get; set; }
        [Required]
        public String Titulo { get; set; }
        [Required]
        public String Diretor { get; set; }
        [Required]
        public String Elenco { get; set; }
        [Required]
        public String Pais { get; set; }
        [Required]
        public int Ano { get; set; }
    }
}
