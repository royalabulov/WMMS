using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WMMS.BLL.Model.DTO_s.RegisterDTO_s;
using WMMS.BLL.Model.GenericResponseApi;
using WMMS.BLL.Services.Interface;
using WMMS.Domain.Entities;

namespace WMMS.BLL.Services.Implementation
{
	public class RegisterService : IRegisterService
	{
		private readonly UserManager<AppUser> userManager;
		private readonly IMapper mapper;

		public RegisterService(UserManager<AppUser> userManager, IMapper mapper)
		{
			this.userManager = userManager;
			this.mapper = mapper;
		}


		public async Task<GenericResponseApi<bool>> CreateMarketManager(CreateMarketManagerDTO createMarketManager)
		{
			var response = new GenericResponseApi<bool>();

			var existingUser = await userManager.FindByEmailAsync(createMarketManager.Email);

			if (existingUser != null)
			{
				response.Failure("User with this email already exists.", 400);
				return response;
			}

			var mapping = mapper.Map<AppUser>(createMarketManager);

			var MarketLocationProfile = await userManager.CreateAsync(mapping, createMarketManager.Password);

			if (!MarketLocationProfile.Succeeded)
			{
				response.Failure(MarketLocationProfile.Errors.Select(e => e.Description).ToList());
				return response;
			}

			var addToRoleResult = await userManager.AddToRoleAsync(mapping, "Manager");
			if (!addToRoleResult.Succeeded)
			{
				await userManager.DeleteAsync(mapping);
				response.Failure("Failed to assign role. User creation has been rolled back.");
				return response;
			}

			response.Success(true);
			return response;
		}

		public async Task<GenericResponseApi<bool>> CreateStorekeeper(CreateStorekeeperDTO createStorekeeper)
		{
			var response = new GenericResponseApi<bool>();
			try
			{
				var existingUser = await userManager.FindByEmailAsync(createStorekeeper.Email);
				if (existingUser != null)
				{
					response.Failure("User with this email already exists", 400);
					return response;
				}

				var mapping = mapper.Map<AppUser>(createStorekeeper);

				var MarketWarehouseProfile = await userManager.CreateAsync(mapping, createStorekeeper.Password);

				if (!MarketWarehouseProfile.Succeeded)
				{
					response.Failure(MarketWarehouseProfile.Errors.Select(x => x.Description).ToList());
					return response;
				}

				var addToRoleResult = await userManager.AddToRoleAsync(mapping, "WareHouse");
				if (!addToRoleResult.Succeeded)
				{
					await userManager.DeleteAsync(mapping);
					response.Failure("Failed to assign role. User creation has been rolled back.");
					return response;
				}

				response.Success(true);
			}
			catch (Exception ex)
			{
				response.Failure(ex.Message, 500);
			}

			return response;
		}

		public async Task<GenericResponseApi<List<GetAllUserDTO>>> GetAllUser()
		{
			var response = new GenericResponseApi<List<GetAllUserDTO>>();

			var allRegisterUser = await userManager.Users.ToListAsync();

			if (allRegisterUser == null)
			{
				response.Failure("User not found", 404);
				return response;
			}

			var mapping = mapper.Map<List<GetAllUserDTO>>(allRegisterUser);

			response.Success(mapping);
			return response;

		}

		public async Task<GenericResponseApi<bool>> UserDelete(int id)
		{
			var response = new GenericResponseApi<bool>();

			var getUserId = await userManager.FindByIdAsync(id.ToString());
			if (getUserId == null)
			{
				response.Failure("Id not found", 404);
				return response;
			}

			await userManager.DeleteAsync(getUserId);

			response.Success(true);
			return response;

		}

		public async Task<GenericResponseApi<bool>> UserUpdate(UserUpdateDTO userUpdate)
		{
			var response = new GenericResponseApi<bool>();

			var getUserId = await userManager.FindByIdAsync(userUpdate.Id.ToString());

			if (getUserId == null)
			{
				response.Failure("Id not found", 404);
				return response;
			}

			var mapping = mapper.Map(userUpdate, getUserId);

			await userManager.UpdateAsync(mapping);

			response.Success(true);
			return response;

		}

		public async Task<GenericResponseApi<bool>> AssignRoleToUserAsync(string id, string[] role)
		{
			var response = new GenericResponseApi<bool>();

			var getUserId = await userManager.FindByIdAsync(id.ToString());

			if (getUserId == null)
			{
				response.Failure("Id not found", 404);
				return response;
			}

			var userRole = await userManager.GetRolesAsync(getUserId);

			await userManager.RemoveFromRoleAsync(getUserId, userRole.ToString());
			await userManager.AddToRoleAsync(getUserId, role.ToString());

			response.Success(true);
			return response;
		}

		public async Task UpdateRefreshToken(string refreshToken, AppUser appUser, DateTime accessTokenData)
		{
			if (appUser != null)
			{
				appUser.RefreshToken = refreshToken;
				appUser.ExpireTimeRFT = accessTokenData.AddMinutes(25);
				await userManager.UpdateAsync(appUser);
			}
		}

		public async Task<GenericResponseApi<string[]>> GetRolesToUserAsync(string userIdOrName)
		{
			var response = new GenericResponseApi<string[]>();

			var getUserId = await userManager.FindByIdAsync(userIdOrName);

			if (getUserId == null)
			{
				response.Failure("Id not found", 404);
				return response;
			}

			var userRole = await userManager.GetRolesAsync(getUserId);

			return response;
		}
	}
}
