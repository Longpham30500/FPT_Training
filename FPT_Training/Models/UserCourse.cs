﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FPT_Training.Models
{
	public class UserCourse
	{
    [Key, Column(Order = 1)]
    [ForeignKey("User")]
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
    [Key, Column(Order = 2)]
    [ForeignKey("Course")]
    public int CourseId { get; set; }
    public Course Course { get; set; }
  }
}