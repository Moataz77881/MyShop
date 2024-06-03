using Microsoft.EntityFrameworkCore;
using MyShop.DataAccess.Repository;
using MyShop.Web.Data;
using System.Linq.Expressions;


namespace MyShop.DataAccess.Implementation
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		private readonly ApplicationDbContext context;
		private readonly DbSet<T> dbSet;

		public GenericRepository(ApplicationDbContext context)
        {
			this.context = context;
			dbSet = context.Set<T>();
		}
        public void Add(T entity)
		{
			dbSet.Add(entity);
		}

		public IEnumerable<T> GetAll(Expression<Func<T, bool>>? perdicate = null, string? IncludeWord = null)
		{
			IQueryable<T> query = dbSet;
			if (perdicate != null) 
			{
				query = query.Where(perdicate);
			}
			if (IncludeWord != null) 
			{
				foreach(var word in IncludeWord.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
				query.Include(word);
			}
			return query.ToList();
		}

		public T GetFristOrDefult(Expression<Func<T, bool>> perdicte, string? IncludeWord = null)
		{
			IQueryable<T> query = dbSet;
			if (perdicte != null)
			{
				query = query.Where(perdicte);
			}
			if (IncludeWord != null) 
			{
				foreach (var word in IncludeWord.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) 
				{
					query.Include(word);
				}
			}
			return query.FirstOrDefault();
		}

		public void Remove(T entity)
		{
			dbSet.Remove(entity);
		}

		public void RemoveRange(IEnumerable<T> entities)
		{
			dbSet.RemoveRange(entities);
		}
	}
}
