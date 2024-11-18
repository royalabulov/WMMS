using WMMS.Domain.Entities;
using WMMS.Domain.RepositoryInterface;

namespace WMMS.Domain.UnitOfWorkInterface
{
	public interface IUnitOfWork : IDisposable
	{
		IGenericRepository<T> GetRepository<T>() where T : BaseEntity;

		Task<int> Commit();


	}
}
