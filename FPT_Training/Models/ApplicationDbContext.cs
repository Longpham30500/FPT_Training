using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace FPT_Training.Models
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

		public IEnumerable<Category> Categories { get; internal set; }

		public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}