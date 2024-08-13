using MyShop.Business.Services.OrderHeaderService;
using MyShop.Business.Services.ShoppingCartService;
using MyShop.DataAccess.Repository;
using MyShop.Entity.Models;
using Stripe.Checkout;


namespace MyShop.Business.Services.PaymentService
{
	public class PaymentServices : IPaymentService
	{
		private readonly IShoppingCartService serviceCart;
		private readonly IOrderHeaderServices orderHeaderServices;
		private readonly IUnitOfWork unitOfWork;
		private readonly IShoppingCartService shoppingCartService;

		public PaymentServices(IShoppingCartService serviceCart, IOrderHeaderServices orderHeaderServices
			,IUnitOfWork unitOfWork, IShoppingCartService shoppingCartService)
        {
			this.serviceCart = serviceCart;
			this.orderHeaderServices = orderHeaderServices;
			this.unitOfWork = unitOfWork;
			this.shoppingCartService = shoppingCartService;
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
				// add List order details
					//get shopping cartVm
				var shoppingCarts = shoppingCartService.GetAllCart(id);

					//map to model
				List<OrderDetails> orderDetails = new List<OrderDetails>();
				foreach (var item in shoppingCarts.ShoppingCartDTO) 
				{
					orderDetails.Add(new OrderDetails
					{
						Count= item.count,
						orderHeaderId = orderHeader.Id,
						Price = item.Price,
						productId = item.ProductId
					});
				}
				//add Details to database
				unitOfWork.OrderDetail.AddRange(orderDetails);
				unitOfWork.complete();

				//remove List shopping cart Models
				IEnumerable<ShoppingCart> carts = unitOfWork.ShoppingCart.GetAll(perdicate: x => x.ApplicationUserId == id);
				unitOfWork.ShoppingCart.RemoveRange(carts);
				unitOfWork.complete();

			}
		}
	}
}
