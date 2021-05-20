using Microsoft.AspNetCore.Mvc;
using Raje.Data;
using Raje.Models;
using Raje.ViewModel;
using System;
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
                Sinopse = serie.Sinopse
            };

            if (ModelState.IsValid)
            {
                _context.Series.Add(serieInserir);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
