using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Entity.Models
{
	public class OrderHeader
	{
        public int Id { get; set; }
        public string applicationUserId { get; set; }
        public ApplicationUser applicationUser { get; set; }
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
