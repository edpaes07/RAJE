using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Raje.ViewModel
{
    public class UserViewModel
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
}