using WMMS.BLL.Model.DTO_s.MarketInventoryDTO_s;
using WMMS.BLL.Model.GenericResponseApi;
using WMMS.Domain.Entities;

namespace WMMS.BLL.Services.Interface
{
	public interface IMarketInventoryService
	{
		Task<GenericResponseApi<List<GetAllMarketProductDTO>>> AllMarketProduct(int marketId);
		Task<GenericResponseApi<bool>> UpdateMarketProduct(UpdateMarketProductDTO updateMarketProductDTO);
		Task<GenericResponseApi<bool>> DeleteProduct(int productId,int marketId);

		Task AddOrUpdateMarketInventoryAsync(StockTransfer stockTransfer);
		Task<MarketInventory> GetMarketInventoryAsync(int marketId, int productId);
	}
}
