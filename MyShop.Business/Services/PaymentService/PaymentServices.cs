using Azure;
using MyShop.Business.Services.OrderHeaderService;
using MyShop.Business.Services.ShoppingCartService;
using MyShop.DataAccess.Repository;
using MyShop.Entity.Models;
using MyShop.Entity.ViewModel;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Business.Services.PaymentService
{
	public class PaymentServices : IPaymentService
	{
		private readonly IShoppingCartService serviceCart;
		private readonly IOrderHeaderServices orderHeaderServices;
		private readonly IUnitOfWork unitOfWork;

		public PaymentServices(IShoppingCartService serviceCart, IOrderHeaderServices orderHeaderServices
			,IUnitOfWork unitOfWork)
        {
			this.serviceCart = serviceCart;
			this.orderHeaderServices = orderHeaderServices;
			this.unitOfWork = unitOfWork;
		}
        public string CreateCheckoutSession(string userId)
		{
			var cardVm = serviceCart.GetAllCart(userId);
			var domain = "https://localhost:7003/";
			var options = new SessionCreateOptions
			{
				LineItems = new List<SessionLineItemOptions>(),

				Mode = "payment",
				SuccessUrl = domain + $"Customer/Payment/confirmation?id={userId}",
				CancelUrl = domain + $"Customer/Cart/viewShoppingCarts",
			};

			foreach (var item in cardVm.ShoppingCartDTO)
			{
				var sessionLineOption = new SessionLineItemOptions
				{
					PriceData = new SessionLineItemPriceDataOptions
					{
						UnitAmount = (long)(item.Price * 100),
						Currency = "EGP",
						ProductData = new SessionLineItemPriceDataProductDataOptions
						{
							Name = item.Name,
						},
					},
					Quantity = item.count,
				};
				options.LineItems.Add(sessionLineOption);
			}


			var service = new SessionService();

			var session = service.Create(options);

			var userOrderHeader = unitOfWork.OrderHeader.GetFristOrDefult(x => x.applicationUserId == userId);

			if (userOrderHeader == null)
				orderHeaderServices.AddOrderHeader(cardVm, userId, session.Id, session.PaymentIntentId);
			else
				orderHeaderServices.UpdateOrderHeader(cardVm, userId, session.Id, session.PaymentIntentId, userOrderHeader.Id);
			
			return session.Url;

		}

		public void paymentConfirmation(string id)
		{
			var orderHeader = orderHeaderServices.getOrderHeaderById(id);
			var service = new SessionService();
			var session = service.Get(orderHeader.sessionId);

			if (session.PaymentStatus.ToLower() == "paid")
			{
				var model = new OrderHeader
				{
					Id = orderHeader.Id,
					applicationUserId = orderHeader.applicationUserId,
					totalPrice = orderHeader.totalPrice,
					orderDate = DateTime.Now,
					shippingDate = orderHeader.shippingDate,
					orderStatus = orderHeader.orderStatus,
					paymentStatus = "paid",
					paymentDate = DateTime.Now,
					sessionId = session.Id,
					paymentId = session.PaymentIntentId
				
				};
				unitOfWork.OrderHeader.Edit(model);
				unitOfWork.complete();
				IEnumerable<ShoppingCart> carts = unitOfWork.ShoppingCart.GetAll(perdicate: x => x.ApplicationUserId == id);
				unitOfWork.ShoppingCart.RemoveRange(carts);
				unitOfWork.complete();
			}
		}
	}
}
