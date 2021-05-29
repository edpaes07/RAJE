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
    public class LivrosController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public LivrosController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
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

            return View(livros);
        }

        //GET - UPSERT
        public IActionResult Upsert(Guid? id)
        {
            if (id == null)
            {
                LivroViewModel livroNovo = new LivroViewModel();
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

                LivroViewModel livroNovo = new LivroViewModel()
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
            Livro livroInserir = new Livro
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
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (livro.Id == null)
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

            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
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
    }
}