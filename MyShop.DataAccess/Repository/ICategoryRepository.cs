using MyShop.Web.Models;

namespace MyShop.DataAccess.Repository
{
	public interface ICategoryRepository : IGenericRepository<Category>
	{
		void Edit(Category category);
	}
}
