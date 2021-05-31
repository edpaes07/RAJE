using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Raje.ViewModel
{
    public class FilmeViewModel
    {
        public Guid Id { get; set; }

        public String Titulo { get; set; }

        public String Diretor { get; set; }

        public String Elenco { get; set; }

        public String Pais { get; set; }

        public int Ano { get; set; }

        public IFormFile ImagemUpload { get; set; }

        public string ImagemURL { get; set; }

        public String Sinopse { get; set; }

        public bool Ativo { get; set; }
    }
}