using MyShop.Entity.DTOS;
using MyShop.Entity.Models;
using MyShop.Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Business.Services.ProductService
{
	public interface IProductServices
	{
		public void setProduct(productVM product);
		public List<ProductsListDTO> getProduct();
		public void EditProduct(productVM product);
		public ProductDTO getProductById(int id);
		public void deleteProduct(int id);
	}
}
