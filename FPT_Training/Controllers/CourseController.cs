using FPT_Training.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPT_Training.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course
        private ApplicationDbContext _context;
        public CourseController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index(string Search)
        {
            IEnumerable<Course> courses = _context.Courses;
            if (!String.IsNullOrEmpty(Search))
            {
                Search = Search.ToLower();
                courses = courses.Where(x => x.CourseName.ToLower().Contains(Search));
            }
            return View(courses.ToList());
        }
    }
}