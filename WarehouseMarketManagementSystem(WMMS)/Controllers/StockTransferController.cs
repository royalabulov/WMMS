using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using WMMS.BLL.Model.DTO_s.StockTransferDTO_s;
using WMMS.BLL.Services.Interface;

namespace WarehouseMarketManagementSystem_WMMS_.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class StockTransferController : ControllerBase
	{
		private readonly IStockTransferService stockTransferService;

		public StockTransferController(IStockTransferService stockTransferService)
		{
			this.stockTransferService = stockTransferService;
		}

		[HttpGet]
		public async Task<IActionResult> GetWareHouseTransfer(int wareHouseId)
		{
			var result = await stockTransferService.GetWareHouseTransfer(wareHouseId);
			return StatusCode(result.StatusCode, result);
		}

		[HttpGet]
		public async Task<IActionResult> GetMarketTransfer(int marketId)
		{
			var result = await stockTransferService.GetMarketTransfer(marketId);
			return StatusCode(result.StatusCode, result);
		}

		[HttpPost]
		public async Task<IActionResult> CreateStockTransfer(CreateStockTransferDTO createStockTransfer)
		{
			var result = await stockTransferService.CreateStockTransfer(createStockTransfer);
			return StatusCode(result.StatusCode, result);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateStockTransfer(UpdateStockTransfer updateStockTransfer)
		{
			var result = await stockTransferService.UpdateStockTransfer(updateStockTransfer);
			return StatusCode(result.StatusCode, result);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteStockTransfer(int id)
		{
			var result = await stockTransferService.DeleteStockTransfer(id);
			return StatusCode(result.StatusCode, result);
		}
	}
}
