using WMMS.BLL.Model.DTO_s.WareHouseInventoryDTO_s;
using WMMS.BLL.Model.GenericResponseApi;

namespace WMMS.BLL.Services.Interface
{
	public interface IWareHouseInventoryService
	{
		Task<GenericResponseApi<List<GetAllWareHouseProductDTO>>> GetAllWareHouseProduct(int wareHouseId);
		Task<GenericResponseApi<bool>> CreateWareHouseProduct(CreateWareHouseProductDTO createWareHouseProductDTO);
		Task<GenericResponseApi<bool>> UpdateWareHouseProduct(UpdateWareHouseInventoryDTO updateWareHouseProductDTO);
		Task<GenericResponseApi<bool>> RemoveProduct(int ProductId, int WareHouseInventoryId);
	}
}
