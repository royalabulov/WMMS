using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMMS.Domain.Entities
{
	public class StockTransfer : BaseEntity
	{
		public int ProductName {  get; set; }
		public int Quantity { get; set; }
		public DateTime TransferDate { get; set; }


		public int WareHouseId {  get; set; }
		public WareHouse WareHouse { get; set; }

		public int MarketId {  get; set; }
		public Market Market { get; set; }

		public int ProductId {  get; set; }
		public Product Product { get; set; }
	}
}
