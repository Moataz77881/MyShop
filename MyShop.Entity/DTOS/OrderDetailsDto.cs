using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Entity.DTOS
{
	public class OrderDetailsDto
	{
        public ProductDTO productDTO { get; set; }
		public decimal Price { get; set; }
		public int Count { get; set; }
	}
}
