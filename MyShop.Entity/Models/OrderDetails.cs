using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Entity.Models
{
	public class OrderDetails
	{
        public int id { get; set; }
        public int orderHeaderId { get; set; }
        [ValidateNever]
        public OrderHeader orderHeader { get; set; }
        public int productId { get; set; }
        [ValidateNever]
        public Product Product { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
    }
}
