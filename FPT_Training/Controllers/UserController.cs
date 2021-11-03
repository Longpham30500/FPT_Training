using FPT_Training.Models;
using FPT_Training.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPT_Training.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext _context;
        private ApplicationUserManager _userManager;
        public UserController()
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

        public ActionResult MyIndex()
        {
            var userId = User.Identity.GetUserId();
            var newView = new UserCoursesModel
            {
                user = UserManager.FindById(userId),
                courses = _context.UsersCourses
                    .Where(m => m.UserId == userId)
                    .Select(m => m.Course).ToList()
            };
            return View(newView);
        }
    }
}