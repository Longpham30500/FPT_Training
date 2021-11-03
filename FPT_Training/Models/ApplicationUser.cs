using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FPT_Training.Models
{
	// You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
	public class ApplicationUser : IdentityUser
	{
		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
		{
			// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
			var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
			// Add custom user claims here
			return userIdentity;
		}

		[Required(ErrorMessage = "don't leave it blank")]
		[RegularExpression(@"^[0-9a-zA-Z''-'\s]{1,40}$",
 ErrorMessage = "special characters are not allowed.")]
		public string FullName { get; set; }

		[Required(ErrorMessage = "don't leave it blank")]
		public int Age { get; set; }

		[Required(ErrorMessage = "don't leave it blank")]
		public string Address { get; set; }
	}
}