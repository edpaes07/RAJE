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
    public class LivrosController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public LivrosController(UserManager<IdentityUser> userManager, ApplicationDbContext db)
        {
            _db = db;
            _userManager = userManager;
        }

        public IActionResult Index(string nome)
        {
            IEnumerable<Livro> livros = new List<Livro>();

            if (User.IsInRole(WC.AdminRole))
            {
                livros = _db.Livros.ToList().OrderBy(livro => livro.Ativo);
            }
            else
            {
                livros = _db.Livros.ToList().Where(livro => livro.Ativo);
            }

            if (nome != null)
                livros = livros.Where(livro => livro.Titulo.ToLower().Contains(nome.ToLower()));

            return View(livros);
        }

        //GET - UPSERT
        public IActionResult Upsert(Guid? id)
        {
            if (id == null)
            {
                LivroViewModel livroNovo = new();
                //this is for create
                return View(livroNovo);
            }
            else
            {
                var livro = _db.Livros.Find(id);

                if (livro == null)
                {
                    return NotFound();
                }

                LivroViewModel livroNovo = new()
                {
                    Id = livro.Id,
                    Ativo = livro.Ativo,
                    Ano = livro.Ano,
                    Autores = livro.Autores,
                    Pais = livro.Pais,
                    Titulo = livro.Titulo,
                    Sinopse = livro.Sinopse,
                    ImagemURL = livro.ImagemURL
                };

                return View(livroNovo);
            }
        }

        //POST - UPSERT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(LivroViewModel livro)
        {
            Livro livroInserir = new()
            {
                Id = livro.Id,
                Ativo = livro.Ativo,
                Ano = livro.Ano,
                Autores = livro.Autores,
                Pais = livro.Pais,
                Titulo = livro.Titulo,
                Sinopse = livro.Sinopse,
                ImagemURL = livro.ImagemURL
            };

            if (livro.ImagemUpload != null)
            {
                var imgPrefixo = Guid.NewGuid() + "_";

                if (!Util.Util.UploadArquivo(livro.ImagemUpload, imgPrefixo))
                {
                    return View(livro);
                }
                livroInserir.ImagemURL = imgPrefixo + livro.ImagemUpload.FileName;
            }

            if (ModelState.IsValid)
            {
                if (livro.Titulo == null)
                {
                    //Creating
                    _db.Livros.Add(livroInserir);
                }
                else
                {
                    //updating
                    _db.Livros.Update(livroInserir);
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

            Livro livro = _db.Livros.Find(id);
            List<Avaliacao> avalicoes = _db.Avaliacoes.Include(a => a.Livro).Include(a => a.User).Where(a => a.LivroId == id).ToList();

            ListagemViewModel retorno = new()
            {
                Livro = livro,
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
            Livro livro = _db.Livros.Find(id);

            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        //POST - DELETE
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(Guid? id)
        {
            var obj = _db.Livros.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Livros.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //POST - COMENTAR LIVRO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Avaliar(Guid id, int nota, string comentario)
        {
            string returnUrl = Url.Content($"~/Livros/Details/{id}");

            Avaliacao avaliacao = new()
            {
                Nota = nota,
                Comentario = comentario,
                UserId = _userManager.GetUserId(User),
                LivroId = id
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