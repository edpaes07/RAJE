using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Raje.Data;

namespace Raje.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "O campo Nome Completo é obrigatório.")]
            [Display(Name = "Nome Completo")]
            public string FullName { get; set; }

            [Display(Name = "E-mail")]
            public string Email { get; set; }

            [Phone]
            [Required(ErrorMessage = "O campo Número de Telefone é obrigatório.")]
            [Display(Name = "Número de Telefone")]
            public string PhoneNumber { get; set; }

            [Required(ErrorMessage = "O campo Data de Nascimento é obrigatório.")]
            [Display(Name = "Data de Nascimento")]
            public DateTime Birthdate { get; set; }

            [Required(ErrorMessage = "O campo Cidade é obrigatório.")]
            [Display(Name = "Cidade")]
            public string City { get; set; }

            [Required(ErrorMessage = "O campo Estado é obrigatório.")]
            [Display(Name = "Estado")]
            public string State { get; set; }

            [Display(Name = "ImagemUpload")]
            public IFormFile ImagemUpload { get; set; }

            public string Id { get; set; }

            public string ImagemURL { get; set; }
        }

        private async Task LoadAsync(IdentityUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);

            var userCurrent = _context.ApplicationUser.ToList().Where(u => u.Id.Equals(user.Id)).FirstOrDefault();

            Username = userName;

            Input = new InputModel
            {
                Id = userCurrent.Id,
                FullName = userCurrent.FullName,
                PhoneNumber = userCurrent.PhoneNumber,
                Birthdate = userCurrent.Birthdate,
                City = userCurrent.City,
                State = userCurrent.State.ToUpper(),
                ImagemURL = userCurrent.ImagemURL,
                ImagemUpload = null
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Não foi possível carregar o usuário com ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Não foi possível carregar o usuário com ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var userCurrent = _context.ApplicationUser.ToList().Where(u => u.Id.Equals(user.Id)).FirstOrDefault();

            userCurrent.FullName = Input.FullName;
            userCurrent.PhoneNumber = Input.PhoneNumber;
            userCurrent.Birthdate = Input.Birthdate;
            userCurrent.City = Input.City;
            userCurrent.State = Input.State.ToUpper();
            userCurrent.ImagemURL = Input.ImagemURL;

            if (Input.ImagemUpload != null)
            {
                var imgPrefixo = Guid.NewGuid() + "_";

                if (!Util.Util.UploadArquivo(Input.ImagemUpload, imgPrefixo))
                {
                    return Page();
                }
                userCurrent.ImagemURL = imgPrefixo + Input.ImagemUpload.FileName;
            }

            _context.ApplicationUser.Update(userCurrent);
            _context.SaveChanges();


            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Seu perfil foi atualizado";
            return RedirectToPage();
        }
    }
}