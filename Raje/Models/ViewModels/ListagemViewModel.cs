using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Raje.Models.ViewModels
{
    public class ListagemViewModel
    {
        public Filme Filme { get; set; }

        public Livro Livro { get; set; }

        public Serie Serie { get; set; }

        public List<Filme> Filmes { get; set; }

        public List<Livro> Livros { get; set; }

        public List<Serie> Series { get; set; }

        public List<ApplicationUser> Users { get; set; }
    }
}
