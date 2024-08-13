using MyShop.Entity.Models;

namespace MyShop.Web.Models
{
	public class Category
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Discreption { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

    }
}
