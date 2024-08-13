using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyShop.Business.Services.ShoppingCartService;
using System.Security.Claims;

namespace MyShop.Web.Areas.Customer.Controllers
{
	[Area("Customer")]
	[Authorize]
	public class CartController : Controller
	{
		private readonly IShoppingCartService service;

		public CartController(IShoppingCartService service)
        {
			this.service = service;
		}
		[HttpGet]
        public IActionResult viewShoppingCarts()
		{
			var ClaimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = ClaimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

			var res = service.GetAllCart(claim.Value);
			return View(res);
		}
		[HttpGet]
		public IActionResult removeItemCart(int id) 
		{
			service.Remove(id);
			return RedirectToAction("viewShoppingCarts", "Cart", new { area = "Customer"});
		}
		[HttpGet]
		public IActionResult increment(int id) 
		{
			var ClaimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = ClaimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			service.Increment(id, claim.Value);

			return RedirectToAction("viewShoppingCarts", "Cart", new { area = "Customer" });
		}
		[HttpGet]
		public IActionResult decrement(int id)
		{
			var ClaimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = ClaimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			service.decrement(id, claim.Value);

			return RedirectToAction("viewShoppingCarts", "Cart", new { area = "Customer" });
		}

	}
}
