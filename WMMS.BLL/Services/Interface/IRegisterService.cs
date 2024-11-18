using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMMS.BLL.Model.DTO_s.RegisterDTO_s;
using WMMS.BLL.Model.GenericResponseApi;
using WMMS.Domain.Entities;

namespace WMMS.BLL.Services.Interface
{
	public interface IRegisterService 
	{
		Task<GenericResponseApi<List<GetAllUserDTO>>> GetAllUser();
		Task<GenericResponseApi<bool>> CreateMarketManager(CreateMarketManagerDTO createMarketManager);
		Task<GenericResponseApi<bool>> CreateStorekeeper(CreateStorekeeperDTO createStorekeeper);
		Task<GenericResponseApi<bool>> UserUpdate(UserUpdateDTO userUpdate);
		Task<GenericResponseApi<bool>> UserDelete(int id);

	    Task UpdateRefreshToken(string refreshToken,AppUser appUser,DateTime accessTokenData);
		Task<GenericResponseApi<bool>> AssignRoleToUserAsync(string id, string[] role);
		Task<GenericResponseApi<string[]>> GetRolesToUserAsync(string userIdOrName);

	}
}
