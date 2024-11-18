using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMMS.BLL.Model.DTO_s.JwtDTO_s
{
	public class GenerateTokenResponse
	{
		public string Token { get; set; }
		public DateTime ExpireDate { get; set; }
		public string RefreshToken { get; set; }
	}
}
