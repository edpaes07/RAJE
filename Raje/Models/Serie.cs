using System;
using System.ComponentModel.DataAnnotations;

namespace Raje.Models
{
    public class Serie
    {
        public Guid Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Diretor { get; set; }
        [Required]
        public string Elenco { get; set; }
        [Required]
        public string Pais { get; set; }
        [Required]
        public int Anos { get; set; }
        [Required]
        public int NumeroTemporadas { get; set; }
    }
}
