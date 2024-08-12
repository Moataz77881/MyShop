using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.Repository
{
	public interface IUnitOfWork : IDisposable
	{
		ICategoryRepository Category { get; }
		IProductRepository Product { get; }
		IUsers users { get; }
		IShoppingCart ShoppingCart { get; }
		IOrderHeaderRepository OrderHeader { get; }
		IOrderDetailRepository OrderDetail { get; }
		int complete();
	}
}
