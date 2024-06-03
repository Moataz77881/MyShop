
using System.Linq.Expressions;


namespace MyShop.DataAccess.Repository
{
	public interface IGenericRepository<T> where T : class
	{
		IEnumerable<T> GetAll(Expression<Func<T,bool>>? perdicate = null ,string? IncludeWord = null);
		T GetFristOrDefult(Expression<Func<T,bool>> perdicte , string? IncludeWord = null);
		void Add(T entity);
		void Remove(T entity);
		void RemoveRange(IEnumerable<T> entities);
	}
}
