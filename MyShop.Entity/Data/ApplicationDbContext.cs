using Microsoft.EntityFrameworkCore;
using MyShop.Entity.Models;
using MyShop.Web.Models;

namespace MyShop.Web.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}
        public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }

	}
}
