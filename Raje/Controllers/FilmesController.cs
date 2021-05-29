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
    public class FilmesController : Controller
    {
        private readonly ApplicationDbContext _db;
        public FilmesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Filme> filmes = new List<Filme>();

            if (User.IsInRole(WC.AdminRole))
            {
                filmes = _db.Filmes.ToList().OrderBy(filme => filme.Ativo);
            }
            else
            {
                filmes = _db.Filmes.ToList().Where(filme => filme.Ativo);
            }

            return View(filmes);
        }

        //GET - UPSERT
        public IActionResult Upsert(Guid? id)
        {
            if (id == null)
            {
                FilmeViewModel filmeNovo = new();
                //this is for create
                return View(filmeNovo);
            }
            else
            {
                var filme = _db.Filmes.Find(id);
                
                if (filme == null)
                {
                    return NotFound();
                }

                FilmeViewModel filmeNovo = new() 
                {
                    Id = filme.Id,
                    Ativo = filme.Ativo,
                    Ano = filme.Ano,
                    Diretor = filme.Diretor,
                    Elenco = filme.Elenco,
                    Pais = filme.Pais,
                    Titulo = filme.Titulo,
                    Sinopse = filme.Sinopse,
                    ImagemURL = filme.ImagemURL

                };

                return View(filmeNovo);
            }
        }

        //POST - UPSERT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(FilmeViewModel filme)
        {
            Filme filmeInserir = new()
            {
                Id = filme.Id,
                Ativo = filme.Ativo,
                Ano = filme.Ano,
                Diretor = filme.Diretor,
                Elenco = filme.Elenco,
                Pais = filme.Pais,
                Titulo = filme.Titulo,
                Sinopse = filme.Sinopse,
                ImagemURL = filme.ImagemURL
            };

            if (filme.ImagemUpload != null)
            {
                var imgPrefixo = Guid.NewGuid() + "_";

                if (!Util.Util.UploadArquivo(filme.ImagemUpload, imgPrefixo))
                {
                    return View(filme);
                }
                filmeInserir.ImagemURL = imgPrefixo + filme.ImagemUpload.FileName;
            }

            if (ModelState.IsValid)
            {
                if (filme.Titulo == null)
                {
                    //Creating
                    _db.Filmes.Add(filmeInserir);
                }
                else
                {
                    //updating
                    _db.Filmes.Update(filmeInserir);
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
            Filme filme = _db.Filmes.Find(id);

            if (filme == null)
            {
                return NotFound();
            }

            return View(filme);
        }

        //GET - DELETE
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Filme filme = _db.Filmes.Find(id);

            if (filme == null)
            {
                return NotFound();
            }

            return View(filme);
        }

        //POST - DELETE
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(Guid? id)
        {
            var obj = _db.Filmes.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Filmes.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}