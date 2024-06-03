using System.ComponentModel.DataAnnotations;

namespace MyShop.Web.DTOS
{
	public class CategoryDTO
	{
        public int Id { get; set; }
        [Required]
		public string Name { get; set; }
		public string Discreption { get; set; }
		public DateTime Date { get; set; } = DateTime.Now;

	}
}
