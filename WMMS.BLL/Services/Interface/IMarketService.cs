using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMMS.BLL.Model.DTO_s.MarketDTO_s;
using WMMS.BLL.Model.GenericResponseApi;

namespace WMMS.BLL.Services.Interface
{
	public interface IMarketService
	{
		Task<GenericResponseApi<List<GetMarketDTO>>> GetAllMarket();
		Task<GenericResponseApi<bool>> CreateMarket(CreateMarketDTO createMarket);
		Task<GenericResponseApi<bool>> UpdateMarket(UpdateMarketDTO updateMarket);
		Task<GenericResponseApi<bool>> DeleteMarket(int id);
	}
}
