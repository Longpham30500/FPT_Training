﻿using FPT_Training.Models;
using FPT_Training.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPT_Training.Controllers
{
    [Authorize(Roles = Role.TrainingStaff)]
    public class CategoryController : Controller
    {
        // GET: Category
        private ApplicationDbContext _context = new ApplicationDbContext();

        public ActionResult Index(string search)
        {
            IEnumerable<Category> data = _context.Categories;
            if (!String.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                data = data.Where(m => m.Name.ToLower().Contains(search));
            }
            return View(data.ToList());
        }
        public ActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                var isExisted = _context.Categories.SingleOrDefault(m => m.Name == category.Name);
                if (isExisted != null)
                    return View(category);
                _context.Categories.Add(new Category
                {
                    Name = category.Name,
                    Description = category.Description
                });
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UpdateCategory(int Id)
        {
            var data = _context.Categories.SingleOrDefault(m => m.Id == Id);
            return View(data);
        }

        [HttpPost]
        public ActionResult UpdateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                var isExisted = _context.Categories.SingleOrDefault(m => m.Name == category.Name && m.Id != category.Id);
                if (isExisted != null)
                    return View(category);
                var data = _context.Categories.SingleOrDefault(m => m.Id == category.Id);
                data.Name = category.Name;
                data.Description = category.Description;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult DeleteCategory(int Id)
        {
            var data = _context.Categories.SingleOrDefault(m => m.Id == Id);
            _context.Categories.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}