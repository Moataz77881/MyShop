using MyShop.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.Repository
{
	public interface IOrderDetailRepository : IGenericRepository<OrderDetails>
	{
		public void Edit(OrderDetails orderDetails);
	}
}
