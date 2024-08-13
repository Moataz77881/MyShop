using Microsoft.AspNetCore.Mvc.Rendering;
using MyShop.Entity.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Entity.ViewModel
{
	public class productVM
	{
        public ProductDTO product { get; set; }
        public IEnumerable<SelectListItem> categories { get; set; }
    }
}
