using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMMS.BLL.Model.DTO_s.SaleDTO_s
{
	public class CreateSaleDTO
	{
		public List<SaleProductDTO> Products { get; set; }
		public int MarketId {  get; set; }
		public DateTime SaleDate { get; set; }
	}
}
