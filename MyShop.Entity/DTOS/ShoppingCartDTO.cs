using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Entity.DTOS
{
	public class ShoppingCartDTO
	{
		public int id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Image { get; set; }
		public decimal Price { get; set; }
		public int count { get; set; }
    }
}
