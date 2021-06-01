using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Raje.Data;
using Raje.Models;
using Raje.Models.ViewModels;

namespace Raje.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public IActionResult Index(string nome)
        {
            string id = _userManager.GetUserId(User);

            if (id != null)
            {
                var filmes = _db.Filmes.Where(filme => filme.Ativo);
                var series = _db.Series.Where(serie => serie.Ativo);
                var livros = _db.Livros.Where(livro => livro.Ativo);

                if (nome != null)
                {
                    filmes = filmes.Where(filme => filme.Titulo.ToLower().Contains(nome.ToLower()));
                    livros = livros.Where(livro => livro.Titulo.ToLower().Contains(nome.ToLower()));
                    series = series.Where(serie => serie.Titulo.ToLower().Contains(nome.ToLower()));
                }

                ApplicationUser user = _db.ApplicationUser.Where(u => u.Id == id).FirstOrDefault();

                List<Amigo> amigos = _db.Amigos.Where(a => a.UserId == id || a.AmigoId == id).ToList();

                var friendCurrentUserIds = amigos.Where(u => u.Id.ToString() != id).Select(a => a.AmigoId);
                friendCurrentUserIds = friendCurrentUserIds.Concat(amigos.Where(u => u.Id.ToString() != id).Select(a => a.UserId));

                IEnumerable<ApplicationUser> users = _db.ApplicationUser.ToList().Where(u => !friendCurrentUserIds.Contains(u.Id));

                amigos = _db.Amigos.Where(a => a.Ativo).ToList();

                var friendIds = amigos.Where(amigo => amigo.UserId == id).Select(amigo => amigo.AmigoId);
                friendIds = friendIds.Concat(amigos.Where(amigo => amigo.AmigoId == id).Select(amigo => amigo.UserId));

                IEnumerable<ApplicationUser> usersAmigos = _db.ApplicationUser.ToList().Where(user => friendIds.Contains(user.Id));

                List<Avaliacao> avalicoes = _db.Avaliacoes
                                               .Include(a => a.Filme)
                                               .Include(a => a.Livro)
                                               .Include(a => a.Serie)
                                               .Include(a => a.User)
                                               .Where(a => a.UserId == id)
                                               .ToList();

                ListagemViewModel retorno = new()
                {
                    Filmes = filmes.ToList(),
                    Livros = livros.ToList(),
                    Series = series.ToList(),
                    Users = users.ToList(),
                    Amigos = usersAmigos.ToList(),
                    User = user
                };

                return View(retorno);
            }

            return LocalRedirect("~/Identity/Account/Login");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}