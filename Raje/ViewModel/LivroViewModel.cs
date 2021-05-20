using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Raje.ViewModel
{
    public class LivroViewModel
    {
        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Autores { get; set; }

        [Required]
        public string Pais { get; set; }

        [Required]
        public int Ano { get; set; }

        public string ImagemURL { get; set; }
        public IFormFile ImagemUpload { get; set; }

        public String Sinopse { get; set; }
    }
}
