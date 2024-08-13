using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyShop.Entity;
using MyShop.Entity.Models;
using MyShop.Web.Models;

namespace MyShop.Web.Data
{
	public class ApplicationDbContext : IdentityDbContext<IdentityUser>
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}
        public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser> userAccount { get; set; }
		public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<OrderHeader> orderHeaders { get; set; }
        public DbSet<OrderDetails> orderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
		{
			var role = new List<IdentityRole>
			{
				new IdentityRole
				{
					Id = "cd380ebc-0482-497f-9043-ce4c5f329e6f",
					Name = RolesName.Admin.ToString(),
					NormalizedName = RolesName.Admin.ToString().ToUpper()
				},
				new IdentityRole
				{
					Id = "0ad84814-48fe-4300-983d-a50ca9d8b498",
					Name = RolesName.Editor.ToString(),
					NormalizedName = RolesName.Editor.ToString().ToUpper()
				},
				new IdentityRole
				{
					Id = "30428aa2-79dc-4ad4-9eda-daa3e36da255",
					Name = RolesName.Customer.ToString(),
					NormalizedName = RolesName.Customer.ToString().ToUpper()
				}
			};
			builder.Entity<IdentityRole>().HasData(role);
			base.OnModelCreating(builder);
		}

	}
}
