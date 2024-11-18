using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using WMMS.BLL.Model.DTO_s.RoleDTO_s;
using WMMS.BLL.Services.Interface;

namespace WarehouseMarketManagementSystem_WMMS_.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class RoleController : ControllerBase
	{
		private readonly IAppRoleService appRoleService;

		public RoleController(IAppRoleService appRoleService)
        {
			this.appRoleService = appRoleService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllRole()
		{
			var result = await appRoleService.GetAllRoles();
			return StatusCode(result.StatusCode, result);
		}

		[HttpPost]
		public async Task<IActionResult> CreateRole(string roleName)
		{
			var result = await appRoleService.CreateRole(roleName);
			return StatusCode(result.StatusCode, result);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateRole(UpdateRoleDTO updateRole)
		{
			var result = await appRoleService.UpdateRole(updateRole);
			return StatusCode(result.StatusCode, result);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteRole(int Id)
		{
			var result = await appRoleService.DeleteRole(Id);
			return StatusCode(result.StatusCode, result);
		}
    }
}
