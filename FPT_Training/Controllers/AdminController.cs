using FPT_Training.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPT_Training.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        private ApplicationUserManager _userManager;
        public AdminController()
        {
            UserManager = _userManager;
            _context = new ApplicationDbContext();
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}