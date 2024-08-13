using MyShop.Entity.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Entity.ViewModel
{
	public class CartVM
	{
        public List<ShoppingCartDTO> ShoppingCartDTO { get; set; }
		public OrderHeaderDto? OrderHeader { get; set; }
		public decimal TotalCard { get; set; }

    }
}
