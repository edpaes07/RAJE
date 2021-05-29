using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Raje.Data;
using Raje.Models;
using Raje.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;

namespace Raje.Controllers
{
    public class ApplicationUserController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;

        public ApplicationUserController
        (
            ApplicationDbContext db, 
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IEmailSender emailSender
        )
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        [TempData]
        public string StatusMessageTemp { get; set; }

        public IActionResult Index()
        {
            IEnumerable<ApplicationUser> users = _db.ApplicationUser.ToList();

            return View(users);
        }

        //GET - UPSERT
        public IActionResult Upsert(string id)
        {
            if (id == null)
            {
                ApplicationUserViewModel userNovo = new();
                //this is for create
                return View(userNovo);
            }
            else
            {
                var user = _db.ApplicationUser.Find(id);

                if (user == null)
                {
                    return NotFound();
                }

                ApplicationUserViewModel userNovo = new()
                {
                    Id = user.Id,
                    Email = user.Email,
                    FullName = user.FullName,
                    PhoneNumber = user.PhoneNumber,
                    Birthdate = user.Birthdate,
                    City = user.City,
                    State = user.State,
                    ImagemURL = user.ImagemURL,
                    StatusMessage = StatusMessageTemp
                };

                return View(userNovo);
            }
        }

        //POST - UPSERT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<IActionResult> UpsertAsync(ApplicationUserViewModel user)
        {
            string returnUrl = Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ApplicationUser userInserir = new();

            if (user.Id != null)
            {
                userInserir = _db.ApplicationUser.Find(user.Id);
            }
            else
            {
                userInserir.UserName = user.Email;
                userInserir.Email = user.Email;
            }
                
            userInserir.FullName = user.FullName;
            userInserir.PhoneNumber = user.PhoneNumber;
            userInserir.Birthdate = user.Birthdate;
            userInserir.City = user.City;
            userInserir.State = user.State;

            if (user.ImagemUpload != null)
            {
                var imgPrefixo = Guid.NewGuid() + "_";

                if (!Util.Util.UploadArquivo(user.ImagemUpload, imgPrefixo))
                {
                    return View(user);
                }
                userInserir.ImagemURL = imgPrefixo + user.ImagemUpload.FileName;
            }

                if (user.Id == null)
                {
                    //Creating
                    var result = await _userManager.CreateAsync(userInserir, user.Password);

                    if (result.Succeeded)
                    {
                        if (User.IsInRole(WC.AdminRole))
                        {
                            //an admin has logged in and they try to create a new user
                            await _userManager.AddToRoleAsync(userInserir, WC.AdminRole);
                        }
                        else
                        {
                            await _userManager.AddToRoleAsync(userInserir, WC.CustomerRole);
                        }

                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(userInserir);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Identity", userId = user.Id, code, returnUrl },
                            protocol: Request.Scheme);

                        await _emailSender.SendEmailAsync(userInserir.Email, "Confirme seu email",
                            $"Por favor, confirme sua conta <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicando aqui</a>.");

                        if (_userManager.Options.SignIn.RequireConfirmedAccount)
                        {
                            return RedirectToPage("RegisterConfirmation", new { email = userInserir.Email, returnUrl });
                        }
                        else
                        {
                            if (!User.IsInRole(WC.AdminRole))
                            {
                                await _signInManager.SignInAsync(userInserir, isPersistent: false);
                            }
                            else
                            {
                                return RedirectToAction("Index");
                            }
                            return LocalRedirect(returnUrl);
                        }
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                else
                {
                    //updating
                    _db.ApplicationUser.Update(userInserir);
                    _db.SaveChanges();

                    returnUrl = Url.Content($"~/ApplicationUser/Details/{userInserir.Id}");
                    StatusMessageTemp = "Alterações realizadas com sucesso!";
            }

            return LocalRedirect(returnUrl);
        }

        //GET - Details
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                ApplicationUserViewModel userNovo = new();
                //this is for create
                return View(userNovo);
            }
            else
            {
                var user = _db.ApplicationUser.Find(id);

                if (user == null)
                {
                    return NotFound();
                }

                ApplicationUserViewModel userNovo = new()
                {
                    Id = user.Id,
                    Email = user.Email,
                    FullName = user.FullName,
                    PhoneNumber = user.PhoneNumber,
                    Birthdate = user.Birthdate,
                    City = user.City,
                    State = user.State,
                    ImagemURL = user.ImagemURL,
                    StatusMessage = StatusMessageTemp
                };

                return View(userNovo);
            }
        }

        //GET - DELETE
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ApplicationUser user = _db.ApplicationUser.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        //POST - DELETE
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(string id)
        {
            var obj = _db.ApplicationUser.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.ApplicationUser.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //POST - ENVIAR SOLICITACAO AMIZADE
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

        //POST - ACEITAR SOLICITACAO AMIZADE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AceitarAmigo(string id)
        {
            string returnUrl = Url.Content($"~/ApplicationUser/SolicitacoesAmizade");
            string currentUserId = _userManager.GetUserId(User);

            Amigo amigo = _db.Amigos.ToList()
                                    .Where(amigo => 
                                           amigo.Ativo == false &&
                                           amigo.UserId == id   &&
                                           amigo.AmigoId == currentUserId)
                                    .FirstOrDefault();

            if (ModelState.IsValid)
            {
                amigo.Ativo = true;
                _db.Amigos.Update(amigo);
                _db.SaveChanges();
            }

            StatusMessageTemp = "Pedido de amizade aceito com sucesso!";
            return LocalRedirect(returnUrl);
        }

        //POST - RECUSAR SOLICITACAO AMIZADE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RecusarAmigo(string id)
        {
            string returnUrl = Url.Content($"~/ApplicationUser/SolicitacoesAmizade");
            string currentUserId = _userManager.GetUserId(User);

            Amigo amigo = _db.Amigos.ToList()
                                    .Where(amigo =>
                                           amigo.Ativo == false &&
                                           amigo.UserId == id &&
                                           amigo.AmigoId == currentUserId)
                                    .FirstOrDefault();

            if (ModelState.IsValid)
            {
                _db.Amigos.Remove(amigo);
                _db.SaveChanges();
            }

            StatusMessageTemp = "Pedido de amizade aceito com sucesso!";

            return LocalRedirect(returnUrl);
        }

        //GET - AMIGO
        public IActionResult Amigos(string id)
        {
            if (id == null)
            {
                id = _userManager.GetUserId(User);
            }

            IEnumerable<Amigo> amigos = _db.Amigos.ToList().Where(amigo => amigo.Ativo);

            var amigoIds = amigos.Where(amigo => amigo.UserId == id).Select(amigo => amigo.AmigoId);
            amigoIds = amigoIds.Concat(amigos.Where(amigo => amigo.AmigoId == id).Select(amigo => amigo.UserId));

            IEnumerable<ApplicationUser> users = _db.ApplicationUser.ToList().Where(user => amigoIds.Contains(user.Id));

            return View(users);
        }

        //GET - SOLICITACOES
        public IActionResult SolicitacoesAmizade()
        {
            string currentUserId = _userManager.GetUserId(User);

            IEnumerable<Amigo> amigos = _db.Amigos.ToList().Where(amigo => amigo.Ativo == false);

            var amigoIds = amigos.Where(amigo => amigo.AmigoId == currentUserId).Select(amigo => amigo.UserId);

            IEnumerable<ApplicationUser> users = _db.ApplicationUser.ToList().Where(user => amigoIds.Contains(user.Id));

            return View(users);
        }
    }
}