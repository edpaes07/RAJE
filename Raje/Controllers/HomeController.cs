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

        public IActionResult Index()
        {
            string id = _userManager.GetUserId(User);

            List<Filme> filmes = _db.Filmes.Where(filme => filme.Ativo).ToList();
            List<Serie> series = _db.Series.Where(serie => serie.Ativo).ToList();
            List<Livro> livros = _db.Livros.Where(livro => livro.Ativo).ToList();
            List<Amigo> amigos = _db.Amigos.Where(amigo => amigo.Ativo).ToList();

            var amigoIds = amigos.Where(amigo => amigo.UserId == id).Select(amigo => amigo.AmigoId);
            amigoIds = amigoIds.Concat(amigos.Where(amigo => amigo.AmigoId == id).Select(amigo => amigo.UserId));

            List<ApplicationUser> friends = _db.ApplicationUser.Where(user => amigoIds.Contains(user.Id)).ToList();
            ApplicationUser user = _db.ApplicationUser.Where(user => user.Id == id).FirstOrDefault();

            var friendIds = friends.Select(friend => friend.Id);

            var users = _db.ApplicationUser.Where(u => u.Id != user.Id && !friendIds.Contains(u.Id)).ToList();

            ListagemViewModel retorno = new()
            {
                Filmes = filmes,
                Livros = livros,
                Series = series,
                Users = users,
                Amigos = friends,
                User = user
            };

            return View(retorno);
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