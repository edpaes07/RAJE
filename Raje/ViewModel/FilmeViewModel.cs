﻿using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Raje.ViewModel
{
    public class FilmeViewModel
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

        public IFormFile ImagemUpload { get; set; }

        public string ImagemURL { get; set; }

        public String Sinopse { get; set; }

        public bool Ativo { get; set; }
    }
}