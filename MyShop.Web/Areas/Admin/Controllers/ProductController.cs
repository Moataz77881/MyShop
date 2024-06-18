using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyShop.Business.Services.CategoryService;
using MyShop.Business.Services.ImageService;
using MyShop.Business.Services.ProductService;
using MyShop.Entity.DTOS;
using MyShop.Entity.ViewModel;

namespace MyShop.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
	{
		private readonly ICategoryService categoryService;
		private readonly IProductServices productServices;
		private readonly IWebHostEnvironment webHost;
		private readonly IImage image;
		private string folderName = "img/Products";

		public ProductController(ICategoryService categoryService, IProductServices productServices
			, IWebHostEnvironment webHost, IImage image)
		{
			this.categoryService = categoryService;
			this.productServices = productServices;
			this.webHost = webHost;
			this.image = image;
		}

		[HttpGet]
		public IActionResult setProduct()
		{
			productVM productVM = new productVM()
			{
				product = new ProductDTO(),
				categories = categoryService.getCategories().Select(x => new SelectListItem
				{
					Text = x.Name,
					Value = x.Id.ToString()
				})
			};
			return View(productVM);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult setProduct(productVM productVM, IFormFile file)
		{
			var rootPath = webHost.WebRootPath;
			productVM.product.Image =image.path(file, rootPath, folderName);
			productServices.setProduct(productVM);
			TempData["setProduct"] = "Data Has Created Successfully";
			return RedirectToAction("viewProduct", "Product");
		}
		[HttpGet]
		public IActionResult viewProduct() 
		{
			var res = productServices.getProduct();

			return View(res);
		}

		[HttpGet]
		public IActionResult EditProduct(int id)
		{
			var productVM = new productVM
			{
				product = productServices.getProductById(id),
				categories = categoryService.getCategories().Select(x => new SelectListItem
				{
					Text = x.Name,
					Value = x.Id.ToString()
				})
			};
			return View(productVM);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult EditProduct(productVM productVM, IFormFile file)
		{
			string rootPath = webHost.WebRootPath;
			productVM.product.Image = image.path(file,rootPath, folderName);
			productServices.EditProduct(productVM);
			TempData["EditProduct"] = "Data Has Edited Successfully";
			return RedirectToAction("viewProduct", "Product");
		}

		public IActionResult deleteProduct(int id) 
		{
			productServices.deleteProduct(id);
			TempData["deleteProduct"] = "Data Has Deleted Successfully";
			return RedirectToAction("viewProduct", "Product");
		}
	}
}
