using FPT_Training.Models;
using FPT_Training.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public ActionResult TrainerIndex()
        {
            var trainer = _context.Users.OfType<Trainer>();
            return View(trainer.ToList());
        }

        public ActionResult CreateTrainer()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateTrainer(RegisterViewModel model, string Specialty)
        {
            if (ModelState.IsValid)
            {
                var user = new Trainer
                {
                    UserName = model.FullName,
                    Email = model.Email,
                    Age = model.Age,
                    Address = model.Address,
                    Specialty = Specialty
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, Role.Trainer);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("TrainerIndex");
                }
                AddErrors(result);
            }
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public ActionResult UpdateTrainer(string Id)
        {
            var userSelected = UserManager.FindById(Id);
            return View(userSelected);
        }

        [HttpPost]
        public ActionResult UpdateTrainer(Trainer user, string newPassword)
        {
            if (ModelState.IsValid)
            {
                var editUser = _context.Users.OfType<Trainer>().SingleOrDefault(m => m.Id == user.Id);
                if (editUser != null)
                {
                    editUser.UserName = user.UserName;
                    editUser.Email = user.Email;
                    editUser.Age = user.Age;
                    editUser.Address = user.Address;
                    editUser.Specialty = user.Specialty;
                    if (!String.IsNullOrEmpty(newPassword))
                    {
                        var token = UserManager.GeneratePasswordResetToken(user.Id);
                        UserManager.ResetPassword(user.Id, token, newPassword);
                    }
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("TrainerIndex");
        }

        public ActionResult DeleteUser(string Id)
        {
            var user = UserManager.FindById(Id);
            UserManager.Delete(user);
            return RedirectToAction("TrainerIndex");
        }

        public ActionResult TrainingStaffIndex()
        {
            var newView = _context.Users.OfType<TrainingStaff>().ToList();
            return View(newView);
        }
    }
}