using MyShop.Entity.DTOS;
using MyShop.Entity.ViewModel;


namespace MyShop.Business.Services.ShoppingCartService
{
	public interface IShoppingCartService
	{
		public void AddToCart(ItemVM item);
		public CartVM GetAllCart(string userId);
		public void Remove(int itemCartId);
		public void Increment(int cartId,string userId);
		public void decrement(int cartId, string userId);

	}
}
