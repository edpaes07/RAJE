using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Raje.Models;

namespace Raje.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "E-mail")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "O {0} deve ter pelo menos {2} e no máximo {1} caracteres.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Senha")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirme a Senha")]
            [Compare("Password", ErrorMessage = "A senha e a senha de confirmação não correspondem.")]
            public string ConfirmPassword { get; set; }

            [Display(Name = "Nome completo")]
            public string FullName { get; set; }

            [Display(Name = "Número de telefone")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Data de Nascimento")]
            public DateTime Birthdate { get; set; }

            [Display(Name = "Cidade")]
            public string City { get; set; }

            [Display(Name = "Estado")]
            public string State { get; set; }

            [Display(Name = "ImagemUpload")]
            public IFormFile ImagemUpload { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if(!await _roleManager.RoleExistsAsync(WC.AdminRole))
            {
                await _roleManager.CreateAsync(new IdentityRole(WC.AdminRole));
                await _roleManager.CreateAsync(new IdentityRole(WC.CustomerRole));
            }

            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
                        returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
   
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser 
                {   
                    UserName = Input.Email, 
                    Email = Input.Email, 
                    PhoneNumber=Input.PhoneNumber,
                    FullName = Input.FullName,
                    Birthdate = Input.Birthdate,
                    City = Input.City,
                    State = Input.State
                };

                if (Input.ImagemUpload != null)
                {
                    var imgPrefixo = Guid.NewGuid() + "_";
                    Util.Util.UploadArquivo(Input.ImagemUpload, imgPrefixo);
                    user.ImagemURL = imgPrefixo + Input.ImagemUpload.FileName;
                }
                    
                var result = await _userManager.CreateAsync(user, Input.Password);
      
                if (result.Succeeded)
                {
                    if (User.IsInRole(WC.AdminRole))
                    {
                        //an admin has logged in and they try to create a new user
                        await _userManager.AddToRoleAsync(user, WC.AdminRole);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, WC.CustomerRole);
                    }
                    
                    _logger.LogInformation("O usuário criou uma nova conta com senha.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirme seu email",
                        $"Por favor, confirme sua conta <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicando aqui</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        if (!User.IsInRole(WC.AdminRole))
                        {
                            await _signInManager.SignInAsync(user, isPersistent: false);
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

            return Page();
        }
    }
}