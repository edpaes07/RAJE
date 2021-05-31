using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace Raje.ViewModel
{
    public class ApplicationUserViewModel
    {
        public string Id { get; set; }

        [EmailAddress(ErrorMessage = "O campo Email não é um endereço de email válido.")]
        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "A {0} deve ter pelo menos {2} e no máximo {1} caracteres.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "O campo Confirme a Senha é obrigatório.")]
        [Display(Name = "Confirme a Senha")]
        [Compare("Password", ErrorMessage = "A senha e a senha de confirmação não correspondem.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "O campo Nome completo é obrigatório.")]
        [Display(Name = "Nome completo")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "O campo Número de telefone é obrigatório.")]
        [Display(Name = "Número de telefone")]
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

        public string ImagemURL { get; set; }

        [TempData]
        public string StatusMessage { get; set; }
    }
}