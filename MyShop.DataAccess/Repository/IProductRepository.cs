using MyShop.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.Repository
{
	public interface IProductRepository : IGenericRepository<Product>
	{
		public void Edit(Product product);
	}
}
