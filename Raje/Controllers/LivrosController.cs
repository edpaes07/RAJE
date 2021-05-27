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
            IEnumerable<Livro> livros = new List<Livro>();

            if (User.IsInRole(WC.AdminRole))
            {
                livros = _context.Livros.ToList().OrderBy(livro => livro.Ativo);
            }
            else
            {
                livros = _context.Livros.ToList().Where(livro => livro.Ativo = true);   
            }

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
                Titulo = livro.Titulo,
                Ativo = true
            };


            if (ModelState.IsValid)
            {
                _context.Livros.Add(livroInserir);
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
            Livro livro = _context.Livros.FirstOrDefault(f => f.Id == id);
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
            var obj = _context.Livros.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _context.Livros.Remove(obj);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}