using Microsoft.AspNetCore.Mvc.Rendering;
using MyShop.DataAccess.Repository;
using MyShop.Entity.DTOS;
using MyShop.Entity.Models;
using MyShop.Entity.ViewModel;
using MyShop.Web.DTOS;
using MyShop.Web.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MyShop.Business.Services.ProductService
{
	public class ProductServices : IProductServices
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IProductRepository productRepository;

		public ProductServices(IUnitOfWork unitOfWork, IProductRepository productRepository)
        {
			this.unitOfWork = unitOfWork;
			this.productRepository = productRepository;
		}

		public void deleteProduct(int id)
		{
			var entity = productRepository.GetFristOrDefult(x => x.Id == id);
			productRepository.Remove(entity);
			unitOfWork.complete();
		}

		public void EditProduct(productVM product)
		{
			var model = new Product
			{
				Id = product.product.Id,
				Name = product.product.Name,
				Image = product.product.Image,
				CategoryId = product.product.CategoryId,
				Description = product.product.Description,
				Price = product.product.Price
			};
			unitOfWork.Product.Edit(model);
			unitOfWork.complete();
		}

		public List<ProductsListDTO> getProduct()
		{
			//var res = productRepository.GetAll(null, "Categories");
			var res = unitOfWork.Product.GetAll(IncludeWord: "Category");
			//map model to product dto
			List<ProductsListDTO> DTO = new List<ProductsListDTO>();

			foreach (var product in res) 
			{
				DTO.Add(new ProductsListDTO 
				{
					Id = product.Id,
					Name = product.Name,
					Description = product.Description,
					Image =product.Image,
					Price = product.Price,
					Category = new CategoryDTO 
					{
						Name = product.Category.Name,
						Date = product.Category.Date,
						Discreption = product.Category.Discreption,
					}
				});
			}
			return DTO;
			
		}

		public ProductDTO getProductById(int id)
		{
			var productModel = unitOfWork
				.Product
				.GetFristOrDefult(x => x.Id == id,IncludeWord: "Category");
			
			var productDTO = new ProductDTO
			{
				Id = productModel.Id,
				Name = productModel.Name,
				Description = productModel.Description,
				Image = productModel.Image,
				Price = productModel.Price,
				CategoryId = productModel.CategoryId,
				Category = new CategoryDTO 
				{
					Name = productModel.Category.Name,
				}
			};

			return productDTO;	
		}

		public void setProduct(productVM product)
		{
			//map product vm to product model
			var model = new Product
			{
				Name = product.product.Name,
				Description = product.product.Description,
				Image = product.product.Image,
				Price = product.product.Price,
				CategoryId = product.product.CategoryId,
			};
			productRepository.Add(model);
			unitOfWork.complete();
		}
		
	}
}
