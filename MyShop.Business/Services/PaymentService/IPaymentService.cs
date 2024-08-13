using MyShop.Entity.ViewModel;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Business.Services.PaymentService
{
	public interface IPaymentService
	{
		public string CreateCheckoutSession(string userId);
		public void paymentConfirmation(string id);
	}
}
