using Microsoft.AspNetCore.Mvc;
using Raje.Data;
using Raje.Models;
using Raje.ViewModel;
using System;
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
        public IActionResult Adicionar(FilmeViewModel filme)
        {
            var imgPrefixo = Guid.NewGuid() + "_";

            if (!Util.Util.UploadArquivo(filme.ImagemUpload, imgPrefixo))
            {
                return View(filme);
            }

            Filme filmeInserir = new Filme
            {
                Ano = filme.Ano,
                Diretor = filme.Diretor,
                Elenco = filme.Elenco,
                Pais = filme.Pais,
                Titulo = filme.Titulo,
                Sinopse = filme.Sinopse,
                ImagemURL = imgPrefixo + filme.ImagemUpload.FileName

            };

            if (ModelState.IsValid)
            {
                _context.Filmes.Add(filmeInserir);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
