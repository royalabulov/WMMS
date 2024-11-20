using Microsoft.AspNetCore.Identity;

namespace WMMS.Domain.Entities
{
	public class AppUser : IdentityUser<int>
	{
		public string? RefreshToken { get; set; }
		public DateTime? ExpireTimeRFT { get; set; }


		public Market? Market { get; set; }
		public WareHouse? WareHouse { get; set; }
	}
}
