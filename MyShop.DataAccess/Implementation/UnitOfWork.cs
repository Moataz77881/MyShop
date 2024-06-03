using MyShop.DataAccess.Repository;
using MyShop.Entity.Models;
using MyShop.Web.Data;

namespace MyShop.DataAccess.Implementation
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext context;
		public ICategoryRepository Category { get; private set; }

		public IProductRepository Product { get; }

		public UnitOfWork(ApplicationDbContext context, ICategoryRepository categoryRepository,
			IProductRepository productRepository)
        {
			this.context = context;
			Category = categoryRepository;
			Product = productRepository;
		}

        public int complete()
		{
			return context.SaveChanges();
		}

		public void Dispose()
		{
			context.Dispose();
		}
	}
}
