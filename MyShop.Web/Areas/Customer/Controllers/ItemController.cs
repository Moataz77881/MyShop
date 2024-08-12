using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyShop.Business.Services.ProductService;
using MyShop.Business.Services.ShoppingCartService;
using MyShop.Entity;
using MyShop.Entity.DTOS;
using MyShop.Entity.ViewModel;
using System.Security.Claims;

namespace MyShop.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ItemController : Controller
    {
        private readonly IProductServices productServices;
		private readonly IShoppingCartService shoppingCartService;

		public ItemController(IProductServices productServices , IShoppingCartService shoppingCartService)
        {
            this.productServices = productServices;
			this.shoppingCartService = shoppingCartService;
		}
        public IActionResult getAllItems()
        {
            var products = productServices.getProduct();
            return View(products);
        }
        [HttpGet]
        public IActionResult itemSelected(int id) 
        {
            var item = new ItemVM
            {
                Count = 1,
                product = productServices.getProductById(id)
			};
            return View(item);
        }
        [HttpPost]
        [Authorize(Roles = nameof(RolesName.Customer))]
        public IActionResult itemSelected(ItemVM item) 
        {
            var ClaimsIdentity = (ClaimsIdentity) User.Identity;
            var claim = ClaimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            item.ApplicationUserId = claim.Value;

            shoppingCartService.AddToCart(item);

			return RedirectToAction("getAllItems", "Item", new { area = "Customer" });
        }

    }
}
