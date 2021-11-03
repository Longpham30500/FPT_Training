using FPT_Training.Models;
using FPT_Training.Utils;
using FPT_Training.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPT_Training.Controllers
{
	[Authorize(Roles = Role.TrainingStaff)]
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

		public ActionResult CreateCourse()
		{
			var newView = new CourseCategoriesModel
			{
				categories = _context.Categories.ToList()
			};
			return View(newView);
		}

		[HttpPost]
		public ActionResult CreateCourse(CourseCategoriesModel courseCategories)
		{
			if (ModelState.IsValid)
			{
				var isExisted = _context.Courses.SingleOrDefault(m => m.CourseName == courseCategories.course.CourseName);
				if (isExisted != null)
					return View(new CourseCategoriesModel
					{
						categories = _context.Categories.ToList()
					});
				var newCourse = new Course()
				{
					CourseName = courseCategories.course.CourseName,
					Description = courseCategories.course.Description,
					CategoryId = courseCategories.Id,
				};
				var data = _context.Courses.Add(newCourse);
			}
			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult UpdateCourse(int id)
		{
			var course = _context.Courses.SingleOrDefault(x => x.Id == id);
			return View(course);
		}

		[HttpPost]
		public ActionResult UpdateCourse(Course course)
		{
			if (ModelState.IsValid)
			{
				var isExisted = _context.Courses.SingleOrDefault(m => m.CourseName == course.CourseName && m.Id != course.Id);

				if (isExisted != null)
					return View(course);
				var oldCourse = _context.Courses.SingleOrDefault(x => x.Id == course.Id);
				oldCourse.CourseName = course.CourseName;
				oldCourse.Description = course.Description;
				_context.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(course);
		}

		public ActionResult DeleteCourse(int CourseId)
		{
			var course = _context.Courses.SingleOrDefault(x => x.Id == CourseId);
			_context.Courses.Remove(course);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}