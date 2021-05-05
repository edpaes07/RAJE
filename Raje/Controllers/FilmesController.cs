using Microsoft.AspNetCore.Mvc;
using Raje.Data;
using Raje.Models;
using System.Linq;

namespace Raje.Controllers
{
    public class FilmesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FilmesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var filmes = _context.Filmes.ToList();

            return View(filmes);
        }


        public IActionResult Adicionar()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Adicionar(Filme filme)
        {
            if (ModelState.IsValid)
            {
                _context.Filmes.Add(filme);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
