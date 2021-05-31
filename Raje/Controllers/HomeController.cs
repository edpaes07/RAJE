using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
                List<Amigo> amigos = _db.Amigos.ToList();

                if (nome != null)
                {
                    filmes = filmes.Where(filme => filme.Titulo.ToLower().Contains(nome.ToLower()));
                    livros = livros.Where(livro => livro.Titulo.ToLower().Contains(nome.ToLower()));
                    series = series.Where(serie => serie.Titulo.ToLower().Contains(nome.ToLower()));
                }
                    
                var amigoIds = amigos.Where(amigo => amigo.UserId == id).Select(amigo => amigo.AmigoId);
                amigoIds = amigoIds.Concat(amigos.Where(amigo => amigo.AmigoId == id).Select(amigo => amigo.UserId));

                List<ApplicationUser> friends = _db.ApplicationUser.Where(user => amigoIds.Contains(user.Id)).ToList();
                ApplicationUser user = _db.ApplicationUser.Where(user => user.Id == id).FirstOrDefault();

                var friendIds = friends.Select(friend => friend.Id);

                var users = _db.ApplicationUser.Where(u => u.Id != user.Id && !friendIds.Contains(u.Id));

                ListagemViewModel retorno = new()
                {
                    Filmes = filmes.ToList(),
                    Livros = livros.ToList(),
                    Series = series.ToList(),
                    Users = users.ToList(),
                    Amigos = friends.ToList(),
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