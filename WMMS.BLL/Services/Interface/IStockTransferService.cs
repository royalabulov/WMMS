using WMMS.BLL.Model.DTO_s.StockTransferDTO_s;
using WMMS.BLL.Model.GenericResponseApi;

namespace WMMS.BLL.Services.Interface
{
	public interface IStockTransferService
	{
		Task<GenericResponseApi<List<List<GetWareHouseTransferDTO>>>> GetWareHouseTransfer(int wareHouseId);
		Task<GenericResponseApi<List<List<GetMarketTransferDTO>>>> GetMarketTransfer(int marketId);
		Task<GenericResponseApi<bool>> CreateStockTransfer(CreateStockTransferDTO createStockTransfer);
		Task<GenericResponseApi<bool>> UpdateStockTransfer(UpdateStockTransfer updateStockTransfer);
		Task<GenericResponseApi<bool>> DeleteStockTransfer(int id);
	}
}
