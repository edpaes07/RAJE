using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Filme> filmes = _db.Filmes.ToList().Where(filme => filme.Ativo = true);
            IEnumerable<Serie> series = _db.Series.ToList().Where(serie => serie.Ativo = true);
            IEnumerable<Livro> livros = _db.Livros.ToList().Where(livro => livro.Ativo = true);

            ListagemViewModel retorno = new ListagemViewModel
            {
                Filmes = filmes.ToList(),
                Livros = livros.ToList(),
                Series = series.ToList()
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
