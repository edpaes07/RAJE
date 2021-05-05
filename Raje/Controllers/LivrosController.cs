using Microsoft.AspNetCore.Mvc;
using Raje.Data;
using Raje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Raje.Controllers
{
    public class LivrosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LivrosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var livros = _context.Livros.ToList();

            return View(livros);
        }


        public IActionResult Adicionar()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Adicionar(Livro livro)
        {
            if (ModelState.IsValid)
            {
                _context.Livros.Add(livro);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
