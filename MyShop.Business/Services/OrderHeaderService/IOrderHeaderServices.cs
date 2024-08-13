using MyShop.Entity.DTOS;
using MyShop.Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Business.Services.OrderHeaderService
{
	public interface IOrderHeaderServices
	{
		public void AddOrderHeader(CartVM cartVM, 
			string userId, string sessionId, string PaymentIntentId);

		public OrderHeaderDto getOrderHeaderById(string? userId);
		public void UpdateOrderHeader(CartVM cartVM,
			string userId, string sessionId, string PaymentIntentId, int headerId);

	}
}
