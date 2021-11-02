using FPT_Training.Models;
using FPT_Training.ViewModels;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPT_Training.Controllers
{
    public class StaffController : Controller
    {
    private ApplicationDbContext _context;
    private ApplicationUserManager _userManager;
    public StaffController()
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

    public ActionResult TraineeIndex(string search)
    {
      var trainee = _context.Users.OfType<Trainee>().Select(m => m.Id);
      IEnumerable<UserCoursesModel> view = _context.UsersCourses.Where(m => trainee.Any(x => x == m.UserId))
          .GroupBy(m => m.User,
                  m => m.Course,
                  (key, elem) => new UserCoursesModel
                  {
                    user = key,
                    courses = elem.ToList()
                  });
      if (!String.IsNullOrEmpty(search))
      {
        search = search.ToLower();
        view = view.Where(m =>
                        m.courses.Any(x => x.CourseName.ToLower().Contains(search))
                        || m.user.UserName.ToLower().Contains(search)
                        || m.user.Age.ToString().Contains(search));
      }
      return View(view.ToList());
    }
  }
}