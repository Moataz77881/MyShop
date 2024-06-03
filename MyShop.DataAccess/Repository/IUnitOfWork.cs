﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.Repository
{
	public interface IUnitOfWork : IDisposable
	{
		ICategoryRepository Category { get; }
		IProductRepository Product { get; }
		int complete();
	}
}
