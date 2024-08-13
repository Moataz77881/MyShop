using Microsoft.AspNetCore.Mvc;
using MyShop.Business.Services.orderDetailsService;
using MyShop.Business.Services.OrderHeaderService;
using MyShop.Entity.DTOS;

namespace MyShop.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class OrderController : Controller
	{
		private readonly IOrderHeaderServices orderHeaderServices;
		private readonly IOrderDetailsServices orderDetails;

		public OrderController(IOrderHeaderServices orderHeaderServices, IOrderDetailsServices orderDetails)
        {
			this.orderHeaderServices = orderHeaderServices;
			this.orderDetails = orderDetails;
		}
		[HttpGet]
        public IActionResult Index()
		{
			return View(orderHeaderServices.getAll());
		}

		[HttpGet]
		public IActionResult Details(int Id) 
		{
			List<OrderDetailsDto> listOrderDetail = orderDetails.getOrderDetailById(Id);
			return View(listOrderDetail);
		}
	}
}
