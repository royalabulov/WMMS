using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WMMS.BLL.Model.DTO_s.MarketDTO_s;
using WMMS.BLL.Services.Interface;

namespace WarehouseMarketManagementSystem_WMMS_.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class MarketController : ControllerBase
	{
		private readonly IMarketService marketService;

		public MarketController(IMarketService marketService)
        {
			this.marketService = marketService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllMarket()
		{
			var result = await marketService.GetAllMarket();
			return StatusCode(result.StatusCode, result);
		}

		[HttpPost]
		public async Task<IActionResult> CreateMarket(CreateMarketDTO createMarket)
		{
			var result = await marketService.CreateMarket(createMarket);
			return StatusCode(result.StatusCode, result);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateMarket(UpdateMarketDTO updateMarket)
		{
			var result = await marketService.UpdateMarket(updateMarket);
			return StatusCode(result.StatusCode, result);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteMarket(int id)
		{
			var result = await marketService.DeleteMarket(id);
			return StatusCode(result.StatusCode, result);
		}
    }
}
