using WMMS.BLL.Model.DTO_s.JwtDTO_s;
using WMMS.Domain.Entities;

namespace WMMS.BLL.Services.Interface
{
	public interface ITokenService
	{
		Task<GenerateTokenResponse> GenerateToken(AppUser appUser);
		string CreateRefreshToken();
	}
}
