using System;
using System.ComponentModel.DataAnnotations;

namespace Raje.Models
{
    public class Livro
    {
        public Guid Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Autores { get; set; }
        [Required]
        public string Pais { get; set; }
        [Required]
        public int Ano { get; set; }
    }
}
