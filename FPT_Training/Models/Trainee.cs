﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FPT_Training.Models
{
	public class Trainee : ApplicationUser
	{
		[MaxLength(50)]
		public string Education { get; set; }
	}
}