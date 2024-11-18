using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMMS.DAL.Context;
using WMMS.Domain.Entities;
using WMMS.Domain.RepositoryInterface;

namespace WMMS.DAL.Repositories.EntityRepository
{
    public class MarketInventoryRepository : GenericRepository<MarketInventory>, IMarketInventoryRepository
    {
		private readonly AppDBContext context;

		public MarketInventoryRepository(AppDBContext context) : base(context)
		{
			this.context = context;
		}
	}
    
}
