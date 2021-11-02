using FPT_Training.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FPT_Training.ViewModels
{
	public class UserCoursesModel
	{
		public ApplicationUser user { get; set; }
		public List<Course> courses { get; set; }
	}
}