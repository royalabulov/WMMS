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
	public class StockTransferRepository : GenericRepository<StockTransfer>, IStockTransferRepository
	{
		private readonly AppDBContext context;

		public StockTransferRepository(AppDBContext context) : base(context)
		{
			this.context = context;
		}
	}
}
