using MyShop.Web.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Business.CategoryServes
{
	public interface ICategoryService
	{
		public List<CategoryDTO> getCategories();
		public void setCategory(CategoryDTO categoryDTO);
		public CategoryDTO getCategoryById(int id);
		public void editCategory(CategoryDTO categoryDTO);
		public void deleteCategory(int id);
	}
}
