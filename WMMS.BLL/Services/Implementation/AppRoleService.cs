using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WMMS.BLL.Model.DTO_s.RoleDTO_s;
using WMMS.BLL.Model.GenericResponseApi;
using WMMS.BLL.Services.Interface;
using WMMS.Domain.Entities;

namespace WMMS.BLL.Services.Implementation
{
	public class AppRoleService : IAppRoleService
	{
		private readonly RoleManager<AppRole> roleManager;
		private readonly IMapper mapper;

		public AppRoleService(RoleManager<AppRole> roleManager, IMapper mapper)
		{
			this.roleManager = roleManager;
			this.mapper = mapper;
		}

		public async Task<GenericResponseApi<bool>> CreateRole(string roleName)
		{
			var response = new GenericResponseApi<bool>();

			var role = await roleManager.CreateAsync(new AppRole { Name = roleName });

			if (role.Succeeded)
			{
				response.Success(true);

			}
			else
			{
				var errors = role.Errors.Select(x=>x.Description).ToList();
				response.Failure(errors, 400);
			}
			return response;
		}

		public async Task<GenericResponseApi<bool>> DeleteRole(int id)
		{
			var response = new GenericResponseApi<bool>();

			var roleId = await roleManager.FindByIdAsync(id.ToString());

			if(roleId == null)
			{

				response.Failure("Id not found", 404);
				return response;
			}

			var deleteRole = await roleManager.DeleteAsync(roleId);
			if (deleteRole.Succeeded)
			{
				response.Success(true);
			}
			else
			{
				var errors = deleteRole.Errors.Select(e => e.Description).ToList();
				response.Failure(errors, 400);
			}
			return response;

		}

		public async Task<GenericResponseApi<List<GetRoleDTO>>> GetAllRoles()
		{
			var response = new GenericResponseApi<List<GetRoleDTO>>();

			var role = await roleManager.Roles.ToListAsync();

			if(role == null)
			{
				response.Failure("Roles not found", 404);
				return response;
			}

			var mapping = mapper.Map<List<GetRoleDTO>>(role);
			response.Success(mapping);

			return response;
		}

		public async Task<GenericResponseApi<bool>> UpdateRole(UpdateRoleDTO updateRoleDTO)
		{
			var response = new GenericResponseApi<bool>();

			var roleId = await roleManager.FindByIdAsync(updateRoleDTO.Id.ToString());
			if (roleId == null)
			{
				response.Failure("Id not found", 404);
				return response;
			}

			var mapping = mapper.Map(updateRoleDTO,roleId);
			await roleManager.UpdateAsync(mapping);

			return response;
			
		}
	}
}
