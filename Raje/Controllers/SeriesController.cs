using Microsoft.AspNetCore.Mvc;
using Raje.Data;
using Raje.Models;
using System.Linq;

namespace Raje.Controllers
{
    public class SeriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SeriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var series = _context.Series.ToList();

            return View(series);
        }

        public IActionResult Adicionar()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Adicionar(Serie serie)
        {
            if (ModelState.IsValid)
            {
                _context.Series.Add(serie);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
