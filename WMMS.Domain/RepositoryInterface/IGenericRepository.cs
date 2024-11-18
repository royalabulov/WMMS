using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WMMS.Domain.RepositoryInterface
{
	public interface IGenericRepository<T> where T : class
	{
		Task<T> GetById(int id);
		Task<IEnumerable<T>> GetAll();
		IQueryable<T> GetAsQueryable();
		Task AddAsync(T entity);
		void DeleteAsync(T entity);
		void UpdateAsync(T entity);
		Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
	}
}
