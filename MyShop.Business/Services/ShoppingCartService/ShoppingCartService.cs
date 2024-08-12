using MyShop.DataAccess.Repository;
using MyShop.Entity.DTOS;
using MyShop.Entity.Models;
using MyShop.Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Business.Services.ShoppingCartService
{
	public class ShoppingCartService : IShoppingCartService
	{
		private readonly IUnitOfWork unitOfWork;

		public ShoppingCartService(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}
		public void AddToCart(ItemVM item)
		{
			// map vm to model
			var model = new ShoppingCart
			{
				ApplicationUserId = item.ApplicationUserId,
				count = item.Count,
				ProductId = item.product.Id
			};
			var exsitProduct = unitOfWork.ShoppingCart
				.GetFristOrDefult(x => x.ApplicationUserId == model.ApplicationUserId && x.ProductId == model.ProductId);
			if (exsitProduct != null)
			{
				unitOfWork.ShoppingCart.Edit(exsitProduct, model);
				unitOfWork.complete();
			}
			else
			{
				unitOfWork.ShoppingCart.Add(model);
				unitOfWork.complete();
			}

		}

		public void Increment(int cartid, string userId)
		{
			var existCart = unitOfWork.ShoppingCart.
				GetFristOrDefult(x => x.ApplicationUserId == userId && x.id == cartid);
			var shoppingCart = new ShoppingCart
			{
				ApplicationUserId = userId,
				id = cartid,
				count = existCart.count + 1,
			};

			unitOfWork.ShoppingCart.Edit(existCart, shoppingCart);
			unitOfWork.complete();
		}

		public CartVM GetAllCart(string userId)
		{
			var cartVM = new CartVM();
			List<ShoppingCartDTO> cartDTO = new List<ShoppingCartDTO>();
			var listOfResult = unitOfWork.ShoppingCart
				.GetAll(x => x.ApplicationUserId == userId, "product");

			foreach (var item in listOfResult)
			{
				cartDTO.Add(new ShoppingCartDTO
				{
					id = item.id,
					count = item.count,
					Name = item.product.Name,
					Description = item.product.Description,
					Image = item.product.Image,
					Price = item.product.Price,
				});
				cartVM.TotalCard += item.count * item.product.Price;
			}
			cartVM.ShoppingCartDTO = cartDTO;


			return cartVM;
		}

		public void Remove(int itemCartId)
		{
			var item = unitOfWork.ShoppingCart.GetFristOrDefult(x => x.id == itemCartId);
			if (item != null)
			{
				unitOfWork.ShoppingCart.Remove(item);
				unitOfWork.complete();
			}
		}

		public void decrement(int cartId, string userId)
		{
			var existCart = unitOfWork.ShoppingCart.
				GetFristOrDefult(x => x.ApplicationUserId == userId && x.id == cartId);
			if (existCart.count > 1)
			{
				var shoppingCart = new ShoppingCart
				{
					ApplicationUserId = userId,
					id = cartId,
					count = existCart.count - 1,
				};

				unitOfWork.ShoppingCart.Edit(existCart, shoppingCart);
				unitOfWork.complete();
			}
		}
	}
}
