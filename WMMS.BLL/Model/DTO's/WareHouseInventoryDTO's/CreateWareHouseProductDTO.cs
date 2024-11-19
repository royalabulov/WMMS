using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMMS.BLL.Model.DTO_s.WareHouseInventoryDTO_s
{
	public class CreateWareHouseProductDTO
	{
		public int WareHouseId { get; set; }
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public int ProductQuantity { get; set; }
		public DateTime ArrivalDate { get; set; }
	}
}
