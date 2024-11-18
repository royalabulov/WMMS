using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMMS.BLL.Model.DTO_s.JwtDTO_s;
using WMMS.BLL.Model.DTO_s.LoginDTO_s;
using WMMS.BLL.Model.GenericResponseApi;

namespace WMMS.BLL.Services.Interface
{
	public interface ILoginService
	{
		Task<GenericResponseApi<GenerateTokenResponse>> Login(LoginCreateDTO login);

		Task<GenericResponseApi<bool>> Logout();

		Task<GenericResponseApi<GenerateTokenResponse>> LoginWithRefreshTokenAsync(string refreshToken);

		Task<GenericResponseApi<bool>> PasswordReset(PasswordResetDTO passwordReset);
	}
}
