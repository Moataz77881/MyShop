using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Entity.Models
{
	public class ApplicationUser : IdentityUser
	{
		[Required]
        public string Name { get; set; }
		[Required]
		public string Address { get; set; }
		[Required]
		public string City { get; set; }
		
	}
}
