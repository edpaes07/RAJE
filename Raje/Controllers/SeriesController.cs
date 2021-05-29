using Microsoft.AspNetCore.Hosting;
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
        private readonly ApplicationDbContext _db;
        public SeriesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Serie> series = new List<Serie>();

            if (User.IsInRole(WC.AdminRole))
            {
                series = _db.Series.ToList().OrderBy(serie => serie.Ativo);
            }
            else
            {
                series = _db.Series.ToList().Where(serie => serie.Ativo);
            }

            return View(series);
        }

        //GET - UPSERT
        public IActionResult Upsert(Guid? id)
        {
            if (id == null)
            {
                SerieViewModel serieNovo = new();
                //this is for create
                return View(serieNovo);
            }
            else
            {
                var serie = _db.Series.Find(id);

                if (serie == null)
                {
                    return NotFound();
                }

                SerieViewModel serieNovo = new()
                {
                    Id = serie.Id,
                    Ativo = serie.Ativo,
                    Ano = serie.Ano,
                    Diretor = serie.Diretor,
                    Elenco = serie.Elenco,
                    Pais = serie.Pais,
                    Titulo = serie.Titulo,
                    Sinopse = serie.Sinopse,
                    NumeroTemporadas = serie.NumeroTemporadas,
                    ImagemURL = serie.ImagemURL

                };

                return View(serieNovo);
            }
        }

        //POST - UPSERT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(SerieViewModel serie)
        {
            Serie serieInserir = new()
            {
                Id = serie.Id,
                Ativo = serie.Ativo,
                Ano = serie.Ano,
                Diretor = serie.Diretor,
                Elenco = serie.Elenco,
                Pais = serie.Pais,
                Titulo = serie.Titulo,
                Sinopse = serie.Sinopse,
                NumeroTemporadas = serie.NumeroTemporadas,
                ImagemURL = serie.ImagemURL
            };

            if (serie.ImagemUpload != null)
            {
                var imgPrefixo = Guid.NewGuid() + "_";

                if (!Util.Util.UploadArquivo(serie.ImagemUpload, imgPrefixo))
                {
                    return View(serie);
                }
                serieInserir.ImagemURL = imgPrefixo + serie.ImagemUpload.FileName;
            }

            if (ModelState.IsValid)
            {
                if (serie.Titulo == null)
                {
                    //Creating
                    _db.Series.Add(serieInserir);
                }
                else
                {
                    //updating
                    _db.Series.Update(serieInserir);
                }

                _db.SaveChanges();

            }
            return RedirectToAction("Index");
        }

        //GET - Details
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Serie serie = _db.Series.Find(id);

            if (serie == null)
            {
                return NotFound();
            }

            return View(serie);
        }

        //GET - DELETE
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Serie serie = _db.Series.Find(id);

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
            var obj = _db.Series.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Series.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}