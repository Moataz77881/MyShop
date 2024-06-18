using MyShop.DataAccess.Repository;
using MyShop.Entity.Models;
using MyShop.Web.Data;


namespace MyShop.DataAccess.Implementation
{
	public class ProductRepository : GenericRepository<Product>, IProductRepository
	{
		private readonly ApplicationDbContext context;
		public ProductRepository(ApplicationDbContext context) : base(context)
		{
			this.context = context;
		}

		public void Edit(Product product)
		{
			var res = context.Products.FirstOrDefault(x => x.Id == product.Id);
			if (res != null) 
			{
				res.Id = product.Id; 
				res.Name = product.Name;
				res.Description = product.Description;
				res.Price = product.Price;
				res.Image = product.Image;
			}
		}
	}
}
