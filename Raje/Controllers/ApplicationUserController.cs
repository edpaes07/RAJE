using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Raje.Data;
using Raje.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Raje.Controllers
{
    public class ApplicationUserController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ApplicationUserController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<ApplicationUser> users = new List<ApplicationUser>();
            users = _db.ApplicationUser.ToList();

            return View(users);
        }

        //GET - Details
        public IActionResult Details(string? id)
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
    }
}