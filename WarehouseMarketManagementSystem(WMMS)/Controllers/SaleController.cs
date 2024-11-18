using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WMMS.BLL.Model.DTO_s.SaleDTO_s;
using WMMS.BLL.Services.Interface;

namespace WarehouseMarketManagementSystem_WMMS_.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class SaleController : ControllerBase
	{
		private readonly ISaleService saleService;

		public SaleController(ISaleService saleService)
		{
			this.saleService = saleService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllSale(int marketId, DateTime? startDate = null, DateTime? endDate = null)
		{
			var result = await saleService.GetAllSale(marketId, startDate, endDate);
			return StatusCode(result.StatusCode, result);
		}

		[HttpPost]
		public async Task<IActionResult> CreateSale(CreateSaleDTO createSale)
		{
			var result = await saleService.CreateSale(createSale);
			return StatusCode(result.StatusCode, result);
		}
	}
}
