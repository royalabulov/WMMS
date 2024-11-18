using Microsoft.AspNetCore.Mvc;
using WMMS.BLL.Model.DTO_s.RegisterDTO_s;
using WMMS.BLL.Services.Interface;

namespace WarehouseMarketManagementSystem_WMMS_.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class RegisterController : ControllerBase
	{
		private readonly IRegisterService registerService;

		public RegisterController(IRegisterService registerService)
		{
			this.registerService = registerService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllUser()
		{
			var result = await registerService.GetAllUser();
			return StatusCode(result.StatusCode, result);
		}

		[HttpGet]
		public async Task<IActionResult> GetRolesToUserAsync(string id)
		{
			var result = await registerService.GetRolesToUserAsync(id);
			return StatusCode(result.StatusCode, result);
		}

		[HttpPost]
		public async Task<IActionResult> CreateStorekeeper(CreateStorekeeperDTO createStorekeeperDTO)
		{
			var result = await registerService.CreateStorekeeper(createStorekeeperDTO);
			return StatusCode(result.StatusCode, result);
		}

		[HttpPost]
		public async Task<IActionResult> CreateMarketManager(CreateMarketManagerDTO createMarketManagerDTO)
		{
			var result = await registerService.CreateMarketManager(createMarketManagerDTO);
			return StatusCode(result.StatusCode, result);
		}

		[HttpPost]
		public async Task<IActionResult> AssignRoleToUserAsync(string Id, string[] roles)
		{
			var result = await registerService.AssignRoleToUserAsync($"{Id}", roles);
			return StatusCode(result.StatusCode, result);
		}

	}
}
