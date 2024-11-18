using WMMS.DAL.Context;
using WMMS.Domain.Entities;
using WMMS.Domain.RepositoryInterface;

namespace WMMS.DAL.Repositories.EntityRepository
{
	public class WareHouseInventoryRepository : GenericRepository<WareHouseInventory>, IWareHouseInventoryRepository
	{
		private readonly AppDBContext context;

		public WareHouseInventoryRepository(AppDBContext context) : base(context)
		{
			this.context = context;
		}
	}
}
