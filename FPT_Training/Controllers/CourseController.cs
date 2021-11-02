﻿using FPT_Training.Models;
using FPT_Training.ViewModels;
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
					return View();
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
	}
}