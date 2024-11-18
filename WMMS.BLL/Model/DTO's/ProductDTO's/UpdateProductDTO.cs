using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMMS.BLL.Model.DTO_s.ProductDTO_s
{
	public class UpdateProductDTO
	{
		public int Id {  get; set; }
		public string ProductName { get; set; }
		public decimal Price { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime ExpireDate { get; set; }
	}
}
