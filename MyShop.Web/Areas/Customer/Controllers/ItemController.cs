using Microsoft.AspNetCore.Mvc;
using MyShop.Business.Services.ProductService;
using MyShop.Entity.ViewModel;

namespace MyShop.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ItemController : Controller
    {
        private readonly IProductServices productServices;

        public ItemController(IProductServices productServices)
        {
            this.productServices = productServices;
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
    }
}
