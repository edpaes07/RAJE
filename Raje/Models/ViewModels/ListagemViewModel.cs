using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Raje.Models.ViewModels
{
    public class ListagemViewModel
    {
        public List<Filme> Filmes { get; set; }
        public List<Livro> Livros { get; set; }
        public List<Serie> Series { get; set; }
    }
}
