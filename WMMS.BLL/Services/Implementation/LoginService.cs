using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WMMS.BLL.Model.DTO_s.JwtDTO_s;
using WMMS.BLL.Model.DTO_s.LoginDTO_s;
using WMMS.BLL.Model.GenericResponseApi;
using WMMS.BLL.Services.Interface;
using WMMS.Domain.Entities;

namespace WMMS.BLL.Services.Implementation
{
	public class LoginService : ILoginService
	{
		private readonly SignInManager<AppUser> signInManager;
		private readonly ITokenService tokenService;
		private readonly IRegisterService registerService;
		private readonly UserManager<AppUser> userManager;


		public LoginService(SignInManager<AppUser> signInManager, ITokenService tokenService, IRegisterService registerService, UserManager<AppUser> userManager)
		{
			this.signInManager = signInManager;
			this.tokenService = tokenService;
			this.registerService = registerService;
			this.userManager = userManager;
		}

		public async Task<GenericResponseApi<bool>> Logout()
		{
			var response = new GenericResponseApi<bool>();

			var user = signInManager.Context.User;
			if (user?.Identity?.IsAuthenticated != true)
			{
				response.Failure("No active session found. User is not logged in.", 400);
				return response;
			}

			await signInManager.SignOutAsync();
			response.Success(true);

			return response;
		}


		public async Task<GenericResponseApi<GenerateTokenResponse>> LoginWithRefreshTokenAsync(string refreshToken)
		{
			var response = new GenericResponseApi<GenerateTokenResponse>();
			AppUser user = await userManager.Users.FirstOrDefaultAsync(rf => rf.RefreshToken == refreshToken);

			try
			{

				if (user != null && user.ExpireTimeRFT > DateTime.UtcNow)
				{
					GenerateTokenResponse token = await tokenService.GenerateToken(user);
					await registerService.UpdateRefreshToken(token.RefreshToken, user, token.ExpireDate.AddMinutes(15));

					response.Success(token);
				}
			}
			catch (Exception ex)
			{
				response.Failure($"Error during login with refresh token: {ex.Message}");
			}
			return response;

		}

		public async Task<GenericResponseApi<GenerateTokenResponse>> Login(LoginCreateDTO login)
		{
			var response = new GenericResponseApi<GenerateTokenResponse>();
			var user = await userManager.FindByNameAsync(login.Email);

			SignInResult result = await signInManager.CheckPasswordSignInAsync(user, login.Password, false);

			if (result.Succeeded)
			{
				GenerateTokenResponse tokenResponse = await tokenService.GenerateToken(user);

				await registerService.UpdateRefreshToken(tokenResponse.RefreshToken, user, tokenResponse.ExpireDate.AddMinutes(15));

				response.Success(tokenResponse);
				return response;
			}
			else
			{
				response.Failure("Invalid login attempt.", 401);
				return response;
			}
		}


		public async Task<GenericResponseApi<bool>> PasswordReset(PasswordResetDTO passwordReset)
		{
			var response = new GenericResponseApi<bool>();

			AppUser user = await userManager.FindByEmailAsync(passwordReset.Email);

			try
			{
				if (user != null)
				{
					var data = userManager.ChangePasswordAsync(user, passwordReset.CurrentPassword, passwordReset.NewPassword);

					if (data != null)
					{
						return response;
					}
				}
			}
			catch (Exception ex)
			{
				response.Failure($"Password not renewed: {ex.Message}");
				return response;
			}
			return response;
		}
	}
}

