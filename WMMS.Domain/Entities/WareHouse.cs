using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMMS.Domain.Entities
{
	public class WareHouse : BaseEntity
	{
		public string Name { get; set; }
		public string Location { get; set; }
		public string ContactNumber { get; set; }


		public int AppUserId { get; set; }
		public AppUser User { get; set; }

		public ICollection<Product> Products { get; set; }
		public ICollection<WareHouseInventory> WareHouseInventories { get; set; }
		public ICollection<StockTransfer> StockTransfers { get; set; }

		public Market Market { get; set; }
	}
}
