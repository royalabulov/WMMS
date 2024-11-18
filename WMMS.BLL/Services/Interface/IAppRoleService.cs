using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMMS.BLL.Model.DTO_s.RoleDTO_s;
using WMMS.BLL.Model.GenericResponseApi;

namespace WMMS.BLL.Services.Interface
{
	public interface IAppRoleService
	{
		Task<GenericResponseApi<List<GetRoleDTO>>> GetAllRoles();
		Task<GenericResponseApi<bool>> CreateRole(string roleName);
		Task<GenericResponseApi<bool>> UpdateRole(UpdateRoleDTO updateRole);
		Task<GenericResponseApi<bool>> DeleteRole(int id);
	}
}
