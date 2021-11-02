using FPT_Training.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FPT_Training.ViewModels
{
	public class AssignCourse
	{
		public string Id { get; set; }
		public List<Course> courses { get; set; }
	}
}