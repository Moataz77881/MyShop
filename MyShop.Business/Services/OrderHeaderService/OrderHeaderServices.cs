using MyShop.DataAccess.Repository;
using MyShop.Entity.Models;
using MyShop.Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Business.Services.OrderHeaderService
{
	public class OrderHeaderServices : IOrderHeaderServices
	{
		private readonly IUnitOfWork unitOfWork;

		public OrderHeaderServices(IUnitOfWork unitOfWork)
        {
			this.unitOfWork = unitOfWork;
		}

		public void AddOrderHeader(CartVM cartVM , string userId, string sessionId, string PaymentIntentId)
		{
			var userModel = unitOfWork.users.GetFristOrDefult(x => x.Id == userId);

			var orderHeaderModel = new OrderHeader
			{
				applicationUserId = userId,
				paymentId = PaymentIntentId,
				sessionId = sessionId,
				totalPrice = cartVM.TotalCard,
				paymentStatus="Pading",
			};
			unitOfWork.OrderHeader.Add(orderHeaderModel);
			unitOfWork.complete();
		}
	}
}
