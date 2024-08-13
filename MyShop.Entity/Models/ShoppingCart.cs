using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;


namespace MyShop.Entity.Models
{
	public class ShoppingCart
	{
        public int id { get; set; }
        public int ProductId { get; set; }

        [ValidateNever]
        public Product product { get; set; }
        public string ApplicationUserId { get; set; }

        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        public int count { get; set; }
    }
}
