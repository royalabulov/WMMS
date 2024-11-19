using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WMMS.BLL.Model.DTO_s.MarketInventoryDTO_s;
using WMMS.BLL.Model.DTO_s.RegisterDTO_s;
using WMMS.BLL.Services.Interface;

namespace WarehouseMarketManagementSystem_WMMS_.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class MarketInventoryController : ControllerBase
	{
		private readonly IMarketInventoryService marketInventoryService;

		public MarketInventoryController(IMarketInventoryService marketInventoryService)
		{
			this.marketInventoryService = marketInventoryService;
		}

		[HttpGet]
		public async Task<IActionResult> AllMarketProduct(int marketId)
		{
			var result = await marketInventoryService.AllMarketProduct(marketId);
			return StatusCode(result.StatusCode, result);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateMarketProduct(UpdateMarketProductDTO updateMarketProduct)
		{
			var result = await marketInventoryService.UpdateMarketProduct(updateMarketProduct);
			return StatusCode(result.StatusCode, result);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteProduct(int productId, int marketId)
		{
			var result = await	marketInventoryService.DeleteProduct(productId, marketId);
			return StatusCode(result.StatusCode, result);
		}

	}
}
