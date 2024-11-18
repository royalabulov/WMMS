using WMMS.DAL.Context;
using WMMS.DAL.Repositories;
using WMMS.Domain.Entities;
using WMMS.Domain.RepositoryInterface;
using WMMS.Domain.UnitOfWorkInterface;

namespace WMMS.DAL.UnitOfWorkImplmentation
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly AppDBContext context;
		private Dictionary<Type, object> repositories;

		public UnitOfWork(AppDBContext context)
		{
			this.context = context;
			repositories = new Dictionary<Type, object>();
		}


		public async Task<int> Commit()
		{
			return await context.SaveChangesAsync();
		}

		public void Dispose()
		{
			context.Dispose();
		}

		public IGenericRepository<T> GetRepository<T>() where T : BaseEntity
		{
			if (repositories.ContainsKey(typeof(T)))
			{
				return (IGenericRepository<T>)repositories[typeof(T)];
			}

			IGenericRepository<T> repository = new GenericRepository<T>(context);
			repositories.Add(typeof(T), repository);
			return repository;
		}
	}
}
