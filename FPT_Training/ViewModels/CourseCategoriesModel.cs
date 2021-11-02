using FPT_Training.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FPT_Training.ViewModels
{
	public class CourseCategoriesModel
	{
		public Course course { get; set; }
		public int Id { get; set; }
		public List<Category> categories { get; set; }
	}
}