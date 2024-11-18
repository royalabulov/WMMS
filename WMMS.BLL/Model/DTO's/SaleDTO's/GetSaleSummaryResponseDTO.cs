using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMMS.BLL.Model.DTO_s.SaleDTO_s
{
	public class GetSaleSummaryResponseDTO
	{
		public int TotalQuantity { get; set; }
		public decimal TotalPrice { get; set; }
		public List<GetSaleDTO> DailySales { get; set; }
		public string Month { get; set; }
	}
}
