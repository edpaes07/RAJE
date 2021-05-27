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
    public class ApplicationUserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<ApplicationUser> users = new List<ApplicationUser>();

            users = _context.ApplicationUser.ToList().OrderBy(user => user.FullName);
          
            return View(users);
        }

        public IActionResult Perfil()
        {
            IEnumerable<ApplicationUser> user = new List<ApplicationUser>();

            user = _context.ApplicationUser.ToList().OrderBy(user => user.FullName);

            return View(user);
        }
    }
}