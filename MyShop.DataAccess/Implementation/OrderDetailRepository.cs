using MyShop.DataAccess.Repository;
using MyShop.Entity.Models;
using MyShop.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.Implementation
{
	public class OrderDetailRepository : GenericRepository<OrderDetails>, IOrderDetailRepository
	{
		private readonly ApplicationDbContext context;

		public OrderDetailRepository(ApplicationDbContext context) :base(context)
        {
			this.context = context;
		}
        public void Edit(OrderDetails orderDetails)
		{
			context.orderDetails.Update(orderDetails);
		}
	}
}
