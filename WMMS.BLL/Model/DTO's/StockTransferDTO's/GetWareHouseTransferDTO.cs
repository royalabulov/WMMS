using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMMS.BLL.Model.DTO_s.StockTransferDTO_s
{
	public class GetWareHouseTransferDTO
	{
		public int Id { get; set; }
		public string ProductName { get; set; }
		public int Quantity { get; set; }
		public DateTime TransferDate { get; set; }
	}
}
