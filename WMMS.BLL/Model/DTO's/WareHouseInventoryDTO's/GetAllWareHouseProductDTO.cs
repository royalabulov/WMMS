using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMMS.BLL.Model.DTO_s.WareHouseInventoryDTO_s
{
	public class GetAllWareHouseProductDTO
	{
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public decimal ProductPrice { get; set; }
		public int WareHouseQuantity { get; set; }
		public DateTime ArrivalDate { get; set; }
	}
}
