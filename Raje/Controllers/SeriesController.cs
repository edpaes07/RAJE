using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Raje.Data;
using Raje.Models;
using Raje.Models.ViewModels;
using Raje.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Raje.Controllers
{
    public class SeriesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public SeriesController(UserManager<IdentityUser> userManager, ApplicationDbContext db)
        {
            _db = db;
            _userManager = userManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public IActionResult Index(string nome)
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

            if (nome != null)
                series = series.Where(serie => serie.Titulo.ToLower().Contains(nome.ToLower()));

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

            if (serieInserir.ImagemURL == null)
                serieInserir.ImagemURL = "50bc7e78-0857-4000-a890-dafe2cd74c83_unnamed.jpg";

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
                StatusMessage = "Série cadastrada com sucesso!\n Pendente aprovação do Administrador.";
            }

            return LocalRedirect($"~/Series/Details/{serieInserir.Id}");
        }

        //GET - Details
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Serie serie = _db.Series.Find(id);
            List<Avaliacao> avalicoes = _db.Avaliacoes.Include(a => a.Serie).Include(a => a.User).Where(a => a.SerieId == id).ToList();

            ListagemViewModel retorno = new()
            {
                Serie = serie,
                Avaliacoes = avalicoes
            };

            serie.StatusMessage = StatusMessage;

            return View(retorno);
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

        //POST - COMENTAR SERIE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Avaliar(Guid id, int nota, string comentario)
        {
            string returnUrl = Url.Content($"~/Series/Details/{id}");

            Avaliacao avaliacao = new()
            {
                Nota = nota,
                Comentario = comentario,
                UserId = _userManager.GetUserId(User),
                SerieId = id
            };

            if (ModelState.IsValid)
            {
                //Creating
                _db.Avaliacoes.Add(avaliacao);
                _db.SaveChanges();
            }

            StatusMessage = "Avaliação realizada com sucesso!";

            return LocalRedirect(returnUrl);
        }
    }
}