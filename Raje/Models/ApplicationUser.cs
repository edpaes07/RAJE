using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Raje.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }

        public DateTime Birthdate { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ImagemURL { get; set; }

        [TempData]
        [NotMapped]
        public string StatusMessage { get; set; }
    }
}