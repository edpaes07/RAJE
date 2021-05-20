using Microsoft.AspNetCore.Mvc;
using Raje.Data;
using Raje.Models;
using Raje.ViewModel;
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
        public IActionResult Adicionar(LivroViewModel livro)
        {
            var imgPrefixo = Guid.NewGuid() + "_";

            if (!Util.Util.UploadArquivo(livro.ImagemUpload, imgPrefixo))
            {
                return View(livro);
            }

            Livro livroInserir = new Livro
            {
                ImagemURL = imgPrefixo + livro.ImagemUpload.FileName,
                Ano = livro.Ano,
                Autores = livro.Autores,
                Pais = livro.Pais,
                Sinopse = livro.Sinopse,
                Titulo = livro.Titulo
            };


            if (ModelState.IsValid)
            {
                _context.Livros.Add(livroInserir);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
