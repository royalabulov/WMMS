using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WMMS.DAL.Context;
using WMMS.Domain.RepositoryInterface;

namespace WMMS.DAL.Repositories
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		private readonly AppDBContext context;
		private readonly DbSet<T> table;
		public GenericRepository(AppDBContext context)
        {
			this.context = context;
			table = context.Set<T>();
		}



        public async Task AddAsync(T entity)
		{
			await table.AddAsync(entity);
		}

		public void DeleteAsync(T entity)
		{
			table.Remove(entity);
		}

		public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
		{
			return await table.FirstOrDefaultAsync(predicate);	
		}

		public async Task<IEnumerable<T>> GetAll()
		{
			return await table.ToListAsync();
		}

		public IQueryable<T> GetAsQueryable()
		{
			var query = table.AsQueryable();
			return query;
		}

		public async Task<T> GetById(int id)
		{
			return await table.FindAsync(id);
		}

		public void UpdateAsync(T entity)
		{
			table.Update(entity);
		}
	}
}
