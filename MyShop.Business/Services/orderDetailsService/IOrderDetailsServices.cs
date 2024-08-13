using MyShop.Entity.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Business.Services.orderDetailsService
{
	public interface IOrderDetailsServices
	{
		public List<OrderDetailsDto> getOrderDetailById(int OrderHeaderId);
	}
}
