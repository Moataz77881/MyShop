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
	public class OrderHeaderRepository : GenericRepository<OrderHeader>, IOrderHeaderRepository
	{
		private readonly ApplicationDbContext context;

		public OrderHeaderRepository(ApplicationDbContext context): base(context)
        {
			this.context = context;
		}
        public void Edit(OrderHeader orderHeader)
		{
			var order = context.orderHeaders.FirstOrDefault(x => x.Id == orderHeader.Id);
			if (order != null) 
			{
				order.Carrier = orderHeader.Carrier;
				order.totalPrice = orderHeader.totalPrice;
				order.paymentDate = orderHeader.paymentDate;
				order.shippingDate = orderHeader.shippingDate;
				order.orderDate = orderHeader.orderDate;
				order.applicationUserId = orderHeader.applicationUserId;
				order.trackingNumber = orderHeader.trackingNumber;
				order.sessionId = orderHeader.sessionId;
				order.paymentId = orderHeader.paymentId;
				order.paymentStatus = orderHeader.paymentStatus;
			}
		}

		public void updateOrderStatus(int id ,string orderStatus, string paymentStatus)
		{
			var order = context.orderHeaders.FirstOrDefault(x => x.Id == id);
			if (order != null)
			{
				order.orderStatus = orderStatus;
				order.paymentStatus = paymentStatus;
			}
		}
	}
}
