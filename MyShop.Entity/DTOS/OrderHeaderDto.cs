using MyShop.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Entity.DTOS
{
	public class OrderHeaderDto
	{
		public int Id { get; set; }
		public string applicationUserId { get; set; }
		public decimal totalPrice { get; set; }
		public DateTime orderDate { get; set; }
		public DateTime shippingDate { get; set; }
		public string? orderStatus { get; set; }
		public string? paymentStatus { get; set; }

		public string? trackingNumber { get; set; }
		public string? Carrier { get; set; }

		public DateTime? paymentDate { get; set; }

		//stripe prop
		public string? sessionId { get; set; }
		public string? paymentId { get; set; }

	}
}
