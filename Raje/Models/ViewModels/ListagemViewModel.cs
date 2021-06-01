using System.Collections.Generic;

namespace Raje.Models.ViewModels
{
    public class ListagemViewModel
    {
        public Filme Filme { get; set; }

        public Livro Livro { get; set; }

        public Serie Serie { get; set; }

        public ApplicationUser User { get; set; }

        public Amigo Amigo { get; set; }

        public Avaliacao Avaliacao { get; set; }

        public List<Filme> Filmes { get; set; }

        public List<Livro> Livros { get; set; }

        public List<Serie> Series { get; set; }

        public List<ApplicationUser> Users { get; set; }

        public List<ApplicationUser> Amigos { get; set; }

        public List<Avaliacao> Avaliacoes { get; set; }

        public int AmigosComumQtd { get; set; }
    }
}