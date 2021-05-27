using Microsoft.AspNetCore.Mvc;
using Raje.Data;
using Raje.Models;
using Raje.ViewModel;
using System;
using System.Collections.Generic;
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
            IEnumerable<Serie> series = new List<Serie>();

            if (User.IsInRole(WC.AdminRole))
            {
                series = _context.Series.ToList().OrderBy(serie => serie.Ativo);
            }
            else
            {           
                series = _context.Series.ToList().Where(serie => serie.Ativo = true);
            }

            return View(series);
        }

        public IActionResult Adicionar()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Adicionar(SerieViewModel serie)
        {
            var imgPrefixo = Guid.NewGuid() + "_";

            if (!Util.Util.UploadArquivo(serie.ImagemUpload, imgPrefixo))
            {
                return View(serie);
            }

            var serieInserir = new Serie
            {
                Diretor = serie.Diretor,
                Titulo = serie.Titulo,
                Ano = serie.Ano,
                Elenco = serie.Elenco,
                ImagemURL = imgPrefixo + serie.ImagemUpload.FileName,
                NumeroTemporadas = serie.NumeroTemporadas,
                Pais = serie.Pais,
                Sinopse = serie.Sinopse,
                Ativo = true
            };

            if (ModelState.IsValid)
            {
                _context.Series.Add(serieInserir);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        //GET - DELETE
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Serie serie = _context.Series.FirstOrDefault(f => f.Id == id);
            if (serie == null)
            {
                return NotFound();
            }

            return View(serie);
        }

        //POST - DELETE
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(Guid? id)
        {
            var obj = _context.Series.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _context.Series.Remove(obj);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}