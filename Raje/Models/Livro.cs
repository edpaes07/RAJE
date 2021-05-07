using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public string ImagemURL { get; set; }

        public String Sinopse { get; set; }
    }
}
