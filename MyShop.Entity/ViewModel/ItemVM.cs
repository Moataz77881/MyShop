using MyShop.Entity.DTOS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Entity.ViewModel
{
	public class ItemVM
	{
        [Range(1,10, ErrorMessage ="You must enter value between 1 to 10")]
        public int Count { get; set; }
        public ProductDTO product { get; set; }
		public string? ApplicationUserId { get; set; }
	}
}
