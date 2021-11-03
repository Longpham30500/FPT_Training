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
    [Authorize(Roles = Role.Admin)]
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
                    FullName = model.FullName,
                    UserName = model.Email,
                    Email = model.Email,
                    Age = model.Age,
                    Address = model.Address,
                    Specialty = Specialty
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _context.UsersCourses.Add(new UserCourse
                    {
                        CourseId = _context.Courses.SingleOrDefault(m => m.CourseName == "Not Exist").Id,
                        UserId = user.Id
                    });
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

        [OverrideAuthorization]
        [Authorize(Roles = Role.Trainer + "," + Role.Admin)]
        public ActionResult UpdateTrainer(string Id = null)
        {
            if (Id == null)
            {
                Id = User.Identity.GetUserId();
            }
            var userSelected = UserManager.FindById(Id);
            return View(userSelected);
        }

        [OverrideAuthorization]
        [Authorize(Roles = Role.Trainer + "," + Role.Admin)]
        [HttpPost]
        public ActionResult UpdateTrainer(Trainer user, string newPassword)
        {
            if (ModelState.IsValid)
            {
                var editUser = _context.Users.OfType<Trainer>().SingleOrDefault(m => m.Id == user.Id);
                if (editUser != null)
                {
                    editUser.FullName = user.FullName;
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
            if (User.IsInRole(Role.Trainer)) return RedirectToAction("MyProfile", "User");
            return RedirectToAction("TrainerIndex");
        }

        public ActionResult StaffIndex()
        {
            var newView = _context.Users.OfType<TrainingStaff>().ToList();
            return View(newView);
        }

        public ActionResult CreateStaff()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateStaff(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new TrainingStaff
                {
                    FullName = model.FullName,
                    UserName = model.Email,
                    Email = model.Email,
                    Age = model.Age,
                    Address = model.Address,
                    isTrainingStaff = true
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, Role.TrainingStaff);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("StaffIndex");
                }
                AddErrors(result);
            }
            return View(model);
        }

        public ActionResult UpdateStaff(string Id)
        {
            var userSelected = UserManager.FindById(Id);
            return View(userSelected);
        }

        [HttpPost]
        public ActionResult UpdateStaff(TrainingStaff user, string newPassword = null)
        {
            if (ModelState.IsValid)
            {
                var editUser = _context.Users.OfType<TrainingStaff>().SingleOrDefault(m => m.Id == user.Id);
                if (editUser != null)
                {
                    editUser.FullName = user.FullName;
                    editUser.Email = user.Email;
                    editUser.Age = user.Age;
                    editUser.Address = user.Address;
                    if (!String.IsNullOrEmpty(newPassword))
                    {
                        var token = UserManager.GeneratePasswordResetToken(user.Id);
                        UserManager.ResetPassword(user.Id, token, newPassword);
                    }
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("StaffIndex");
        }

        public ActionResult DeleteUser(string Id)
        {
            var user = UserManager.FindById(Id);
            if(user is Trainer)
            {
                UserManager.Delete(user);
                return RedirectToAction("TrainerIndex");
            }    
            UserManager.Delete(user);
            return RedirectToAction("StaffIndex");
        }
    }
}