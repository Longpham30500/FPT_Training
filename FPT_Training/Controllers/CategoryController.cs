using FPT_Training.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPT_Training.Controllers
{
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
  }
}