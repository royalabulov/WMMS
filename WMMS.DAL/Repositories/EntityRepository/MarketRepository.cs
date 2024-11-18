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
	public class MarketRepository : GenericRepository<Market>, IMarketRepository
	{
		private readonly AppDBContext context;

		public MarketRepository(AppDBContext context) : base(context)
        {
			this.context = context;
		}
    }
}
