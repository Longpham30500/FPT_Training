using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;

namespace FPT_Training.Models
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("fpt-training", throwIfV1Schema: false)
        {
        }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<UserCourse> UsersCourses { get; set; }


    public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}