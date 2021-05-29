using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Raje.Data;
using Raje.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using System;

namespace Raje.Controllers
{
    public class AmigosController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public AmigosController
        (
            ApplicationDbContext db,
            UserManager<IdentityUser> userManager
        )
        {
            _db = db;
            _userManager = userManager;
        }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        [TempData]
        public string StatusMessageTemp { get; set; }

        public IActionResult Amigos()
        {
            string currentUserId = _userManager.GetUserId(User);

            IEnumerable<Amigo> amigos = _db.Amigos.ToList()
                                                  .Where(amigo => amigo.Ativo);

            var amigoIds = amigos.Where(amigo => amigo.UserId == currentUserId).Select(amigo => amigo.AmigoId);
            amigoIds = amigos.Where(amigo => amigo.AmigoId == currentUserId).Select(amigo => amigo.UserId);

            IEnumerable<ApplicationUser> users = _db.ApplicationUser.ToList().Where(user => amigoIds.Contains(user.Id));

            return View(users);
        }

        //POST - AMIGO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AdicionarAmigo(string id)
        {
            string returnUrl = Url.Content($"~/ApplicationUser/Details/{id}");

            Amigo amigo = new()
            {
                UserId = _userManager.GetUserId(User),
                AmigoId = id
            };

            if (ModelState.IsValid)
            {
                //Creating
                _db.Amigos.Add(amigo);
                _db.SaveChanges();
            }

            StatusMessageTemp = "Pedido de amizade enviado com sucesso!";
            return LocalRedirect(returnUrl);
        }
    }
}