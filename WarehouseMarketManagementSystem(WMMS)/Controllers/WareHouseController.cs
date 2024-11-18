using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WMMS.BLL.Model.DTO_s.WareHouseDTO_s;
using WMMS.BLL.Services.Interface;

namespace WarehouseMarketManagementSystem_WMMS_.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class WareHouseController : ControllerBase
	{
		private readonly IWareHouseService wareHouseService;

		public WareHouseController(IWareHouseService wareHouseService)
		{
			this.wareHouseService = wareHouseService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllWareHouse()
		{
			var result = await wareHouseService.GetWareHouse();
			return StatusCode(result.StatusCode, result);
		}

		[HttpPost]
		public async Task<IActionResult> CreateWareHouse(CreateWareHouseDTO createWareHouse)
		{
			var result = await wareHouseService.CreateWareHouse(createWareHouse);
			return StatusCode(result.StatusCode, result);
		}

		[HttpPut]

		public async Task<IActionResult> UpdateWareHouse(UpdateWareHouseDTO updateWareHouse)
		{
			var result = await wareHouseService.UpdateWareHouse(updateWareHouse);
			return StatusCode(result.StatusCode, result);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteWareHouse(int id)
		{
			var result = await wareHouseService.DeleteWareHouse(id);
			return StatusCode(result.StatusCode, result);
		}
	}
}
