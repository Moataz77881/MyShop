using MyShop.DataAccess.Repository;
using MyShop.Entity.Models;
using MyShop.Web.Data;

namespace MyShop.DataAccess.Implementation
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext context;
		public ICategoryRepository Category { get; private set; }

		public IProductRepository Product { get; private set; }
		public IUsers users { get; private set; }

		public IShoppingCart ShoppingCart { get; private set; }

        public IOrderHeaderRepository OrderHeader { get; private set; }
		public IOrderDetailRepository OrderDetail { get; private set; }

		public UnitOfWork(ApplicationDbContext context, ICategoryRepository categoryRepository,
			IProductRepository productRepository,IUsers users,IShoppingCart cart, 
			IOrderHeaderRepository orderHeader, IOrderDetailRepository orderDetail)
        {
			this.context = context;
			Category = categoryRepository;
			Product = productRepository;
			this.users = users;
			ShoppingCart = cart;
			OrderHeader = orderHeader;
			OrderDetail = orderDetail;
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
