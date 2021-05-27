using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Raje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Raje.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Filme> Filmes { get; set; }

        public DbSet<Serie> Series { get; set; }

        public DbSet<Livro> Livros { get; set; }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public DbSet<Amigo> Amigos { get; set; }

        public DbSet<Avaliacao> Avaliacoes { get; set; }
    }
}