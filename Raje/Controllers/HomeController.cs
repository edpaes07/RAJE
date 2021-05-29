using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Raje.Data;
using Raje.Models;
using Raje.Models.ViewModels;
using Raje.Utility;

namespace Raje.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _db = db;
            _userManager = userManager;
        }

        public async System.Threading.Tasks.Task<IActionResult> IndexAsync()
        {
            IEnumerable<Filme> filmes = _db.Filmes.ToList().Where(filme => filme.Ativo);
            IEnumerable<Serie> series = _db.Series.ToList().Where(serie => serie.Ativo);
            IEnumerable<Livro> livros = _db.Livros.ToList().Where(livro => livro.Ativo);

            var user = await _userManager.GetUserAsync(User);
            IEnumerable<ApplicationUser> users = new List<ApplicationUser>();

            if (user != null)
                users = _db.ApplicationUser.Take(3).ToList().Where(u => u.Id != user.Id);
                
            ListagemViewModel retorno = new ListagemViewModel
            {
                Filmes = filmes.ToList(),
                Livros = livros.ToList(),
                Series = series.ToList(),
                Users = users.ToList()
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