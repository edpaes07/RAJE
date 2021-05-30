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
    public class FilmesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public FilmesController(UserManager<IdentityUser> userManager, ApplicationDbContext db)
        {
            _db = db;
            _userManager = userManager;
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
            List<Avaliacao> avalicoes = _db.Avaliacoes.Include(a => a.Filme).Include(a => a.User).Where(a => a.FilmeId == id).ToList();

            ListagemViewModel retorno = new()
            {
                Filme = filme,
                Avaliacoes = avalicoes
            };

            return View(retorno);
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

        //POST - COMENTAR FILME
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Avaliar(Guid id, int nota, string comentario)
        {
            string returnUrl = Url.Content($"~/Filmes/Details/{id}");

            Avaliacao avaliacao = new()
            {
                Nota = nota,
                Comentario = comentario,
                UserId = _userManager.GetUserId(User),
                FilmeId = id
            };

            if (ModelState.IsValid)
            {
                //Creating
                _db.Avaliacoes.Add(avaliacao);
                _db.SaveChanges();
            }

            return LocalRedirect(returnUrl);
        }
    }
}