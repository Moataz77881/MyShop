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
				product.Id = res.Id;
				product.Name = res.Name;
				product.Description = res.Description;
				product.Price = res.Price;
				product.Image = res.Image;
			}
		}
	}
}
