using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMMS.BLL.Model.DTO_s.SaleDTO_s
{
	public class SaleProductDTO
	{
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public int Quantity { get; set; }
		public decimal PricePerUnit { get; set; }
	}
}
