using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [NotMapped]
        public IFormFile Imagem { get; set; }

        public String Sinopse { get; set; }
    }
}
