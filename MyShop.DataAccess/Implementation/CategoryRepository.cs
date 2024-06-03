using MyShop.DataAccess.Repository;
using MyShop.Web.Data;
using MyShop.Web.Models;

namespace MyShop.DataAccess.Implementation
{
	public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
	{
		private readonly ApplicationDbContext context;
		public CategoryRepository(ApplicationDbContext context) : base(context)
		{
			this.context = context;
		}

		public void Edit(Category category)
		{
			var res = context.Categories.FirstOrDefault(x=>x.Id == category.Id);
			if (res != null) 
			{
				res.Name = category.Name;
				res.Discreption = category.Discreption;
			}
		}
	}
}
