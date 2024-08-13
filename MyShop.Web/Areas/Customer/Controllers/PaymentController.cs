using Microsoft.AspNetCore.Mvc;
using MyShop.Business.Services.OrderHeaderService;
using MyShop.Business.Services.PaymentService;
using MyShop.Business.Services.ShoppingCartService;
using MyShop.Entity.ViewModel;
using Stripe.Checkout;
using System.Security.Claims;

namespace MyShop.Web.Areas.Customer.Controllers
{
	[Area("Customer")]
	public class PaymentController : Controller
	{
		private readonly IShoppingCartService serviceCart;
		private readonly IPaymentService paymentService;
		private readonly IOrderHeaderServices orderHeaderServices;

		public PaymentController(IShoppingCartService serviceCart, IPaymentService paymentService,
			IOrderHeaderServices orderHeaderServices)
		{
			this.serviceCart = serviceCart;
			this.paymentService = paymentService;
			this.orderHeaderServices = orderHeaderServices;
		}
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public ActionResult CreateCheckoutSession()
		{
			var ClaimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = ClaimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

			Response.Headers.Add("Location", paymentService.CreateCheckoutSession(claim.Value));

			return new StatusCodeResult(303);
		}

		[HttpGet]
		public IActionResult confirmation(string id) 
		{
			paymentService.paymentConfirmation(id);

			return RedirectToAction("viewShoppingCarts", "Cart", new { area = "Customer" });
		}
	}
}
