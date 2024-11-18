using Microsoft.AspNetCore.Mvc;
using WMMS.BLL.Model.DTO_s.LoginDTO_s;
using WMMS.BLL.Services.Interface;

namespace WarehouseMarketManagementSystem_WMMS_.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private readonly ILoginService loginService;

		public LoginController(ILoginService loginService)
		{
			this.loginService = loginService;
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginCreateDTO loginCreate)
		{
			var result = await loginService.Login(loginCreate);
			return StatusCode(result.StatusCode, result);
		}

		[HttpPost]
		public async Task<IActionResult> LogOut()
		{
			var result = await loginService.Logout();
			return StatusCode(result.StatusCode, result);
		}

		[HttpPost]
		public async Task<IActionResult> LoginWithRefreshTokenAsync(string refreshToken)
		{
			var result = await loginService.LoginWithRefreshTokenAsync(refreshToken);
			return StatusCode(result.StatusCode, result);
		}

		[HttpPost]
		public async Task<IActionResult> PasswordReset(PasswordResetDTO passwordReset)
		{
			var result = await loginService.PasswordReset(passwordReset);
			return StatusCode(result.StatusCode, result);
		}


	}
}
