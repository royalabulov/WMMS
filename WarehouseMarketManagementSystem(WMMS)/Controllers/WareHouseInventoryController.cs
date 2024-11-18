using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WMMS.BLL.Model.DTO_s.WareHouseInventoryDTO_s;
using WMMS.BLL.Services.Interface;

namespace WarehouseMarketManagementSystem_WMMS_.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class WareHouseInventoryController : ControllerBase
	{
		private readonly IWareHouseInventoryService wareHouseInventoryService;

		public WareHouseInventoryController(IWareHouseInventoryService wareHouseInventoryService)
		{
			this.wareHouseInventoryService = wareHouseInventoryService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllWareHouseProduct(int wareHouseId)
		{
			var result = await wareHouseInventoryService.GetAllWareHouseProduct(wareHouseId);
			return StatusCode(result.StatusCode, result);
		}

		[HttpPost]
		public async Task<IActionResult> CreateWareHouseProduct(CreateWareHouseProductDTO createWareHouseProduct)
		{
			var result = await wareHouseInventoryService.CreateWareHouseProduct(createWareHouseProduct);
			return StatusCode(result.StatusCode, result);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateWareHouseProduct(UpdateWareHouseInventoryDTO updateWareHouseProduct)
		{
			var result = await wareHouseInventoryService.UpdateWareHouseProduct(updateWareHouseProduct);
			return StatusCode(result.StatusCode, result);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteProduct(int productId, int WareHouseInventoryId)
		{
			var result = await wareHouseInventoryService.RemoveProduct(productId,WareHouseInventoryId);
			return StatusCode(result.StatusCode, result);
		}

	}
}
