using MyShop.DataAccess.Repository;
using MyShop.Entity.Models;
using MyShop.Web.Data;


namespace MyShop.DataAccess.Implementation
{
	public class ShoppingCartRepository : GenericRepository<ShoppingCart>, IShoppingCart
	{
        public ShoppingCartRepository(ApplicationDbContext context) : base(context) 
        {
        }

		public void Edit(ShoppingCart existCart,ShoppingCart cart)
		{
			existCart.count = cart.count;
		}
	}
}
