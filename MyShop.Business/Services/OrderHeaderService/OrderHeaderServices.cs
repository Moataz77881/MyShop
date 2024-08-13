using MyShop.DataAccess.Repository;
using MyShop.Entity.DTOS;
using MyShop.Entity.Models;
using MyShop.Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Business.Services.OrderHeaderService
{
	public class OrderHeaderServices : IOrderHeaderServices
	{
		private readonly IUnitOfWork unitOfWork;

		public OrderHeaderServices(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		public void AddOrderHeader(CartVM cartVM, string userId, string sessionId, string PaymentIntentId)
		{
			var orderHeaderModel = new OrderHeader
			{
				applicationUserId = userId,
				paymentId = PaymentIntentId,
				sessionId = sessionId,
				totalPrice = cartVM.TotalCard,
				paymentStatus = "Pending",
			};
			unitOfWork.OrderHeader.Add(orderHeaderModel);
			unitOfWork.complete();
		}

		public List<OrderHeaderDto> getAll()
		{
			var listOrderHeaderModel = unitOfWork.OrderHeader.GetAll(IncludeWord: "applicationUser");
			List<OrderHeaderDto> orderHeaderDtos = new List<OrderHeaderDto>();

			foreach (var item in listOrderHeaderModel)
			{
				orderHeaderDtos.Add(new OrderHeaderDto
				{
					Id = item.Id,
					applicationUserDTO = new ApplicationUserDTO
					{
						Name = item.applicationUser.Name,
						Address = item.applicationUser.Address,
						City = item.applicationUser.City,
						PhoneNumber = item.applicationUser.PhoneNumber,
						Email = item.applicationUser.Email
					},
					applicationUserId = item.applicationUserId,
					orderDate = item.orderDate,
					orderStatus = item.orderStatus,
					paymentDate = item.paymentDate,
					paymentStatus = item.paymentStatus,
					paymentId = item.paymentId,
					sessionId = item.sessionId,
					totalPrice = item.totalPrice,

				});
			}
			return orderHeaderDtos;

		}

		public OrderHeaderDto getOrderHeaderById(string? userId)
		{
			var orderHeaderModel = unitOfWork.OrderHeader.GetFristOrDefult(u => u.applicationUserId == userId);


			return new OrderHeaderDto
			{
				Id = orderHeaderModel.Id,
				applicationUserId = orderHeaderModel.applicationUserId,
				totalPrice = orderHeaderModel.totalPrice,
				orderDate = orderHeaderModel.orderDate,
				shippingDate = orderHeaderModel.shippingDate,
				paymentStatus = orderHeaderModel.paymentStatus,
				sessionId = orderHeaderModel.sessionId,
			};
		}

		public void UpdateOrderHeader(CartVM cartVM, string userId, string sessionId, string PaymentIntentId, int headerId)
		{
			var orderHeaderModel = new OrderHeader
			{
				Id = headerId,
				applicationUserId = userId,
				paymentId = PaymentIntentId,
				sessionId = sessionId,
				totalPrice = cartVM.TotalCard,
				paymentStatus = "Pending",
			};
			unitOfWork.OrderHeader.Edit(orderHeaderModel);
			unitOfWork.complete();
		}
	}
}
