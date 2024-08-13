using MyShop.DataAccess.Repository;
using MyShop.Entity.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Business.Services.orderDetailsService
{
	public class OrderDetailsServices : IOrderDetailsServices
	{
		private readonly IUnitOfWork unitOfWork;

		public OrderDetailsServices(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}
		public List<OrderDetailsDto> getOrderDetailById(int OrderHeaderId)
		{
			var listOfOrderDetails = unitOfWork.OrderDetail.GetAll(perdicate: x => x.orderHeaderId == OrderHeaderId, IncludeWord: "Product");
			List<OrderDetailsDto> orderDetailsDtos = new List<OrderDetailsDto>();

			foreach (var orderDetail in listOfOrderDetails)
			{
				orderDetailsDtos.Add(new OrderDetailsDto
				{
					Count = orderDetail.Count,
					Price = orderDetail.Price,
					productDTO = new ProductDTO
					{
						Id=orderDetail.Product.Id,
						CategoryId = orderDetail.Product.CategoryId,
						Description = orderDetail.Product.Description,
						Image = orderDetail.Product.Image,
						Name = orderDetail.Product.Name,
						Price = orderDetail.Product.Price
					}
				});
			}
			return orderDetailsDtos;
		}
	}
}
