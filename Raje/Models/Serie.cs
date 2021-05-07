using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public int Ano { get; set; }

        [Required]
        public int NumeroTemporadas { get; set; }

        public string ImagemURL { get; set; }

        public String Sinopse { get; set; }
    }
}
