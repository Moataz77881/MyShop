using Azure;
using MyShop.Business.Services.OrderHeaderService;
using MyShop.Business.Services.ShoppingCartService;
using MyShop.DataAccess.Repository;
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
				SuccessUrl = domain,
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

			return session.Url;

		}
	}
}
