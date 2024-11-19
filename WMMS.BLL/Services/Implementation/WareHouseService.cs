using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using WMMS.BLL.Model.DTO_s.WareHouseDTO_s;
using WMMS.BLL.Model.GenericResponseApi;
using WMMS.BLL.Services.Interface;
using WMMS.Domain.Entities;
using WMMS.Domain.UnitOfWorkInterface;

namespace WMMS.BLL.Services.Implementation
{
	public class WareHouseService : IWareHouseService
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper mapper;
		private readonly IHttpContextAccessor httpContextAccessor;

		public WareHouseService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
		{
			this.unitOfWork = unitOfWork;
			this.mapper = mapper;
			this.httpContextAccessor = httpContextAccessor;
		}

		public async Task<GenericResponseApi<bool>> CreateWareHouse(CreateWareHouseDTO createWareHouse)
		{
			var response = new GenericResponseApi<bool>();

			var mapping = mapper.Map<WareHouse>(createWareHouse);

			var currentWareHouse = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (currentWareHouse == null)
			{
				response.Failure("currentCompany not found", 404);

				return response;
			}

			mapping.AppUserId = int.Parse(currentWareHouse);

			await unitOfWork.GetRepository<WareHouse>().AddAsync(mapping);
			await unitOfWork.Commit();

			response.Success(true);
			return response;
		}

		public async Task<GenericResponseApi<bool>> DeleteWareHouse(int id)
		{
			var response = new GenericResponseApi<bool>();

			var getWareHouseId = await unitOfWork.GetRepository<WareHouse>().GetById(id);

			if (getWareHouseId == null)
			{
				response.Failure("Id not found", 404);
				return response;
			}
			unitOfWork.GetRepository<WareHouse>().DeleteAsync(getWareHouseId);
			await unitOfWork.Commit();
			response.Success(true);
			return response;
		}

		public async Task<GenericResponseApi<List<GetWareHouseDTO>>> GetWareHouse()
		{
			var response = new GenericResponseApi<List<GetWareHouseDTO>>();

			var getAllWareHouse = await unitOfWork.GetRepository<WareHouse>().GetAll();

			if (getAllWareHouse == null)
			{
				response.Failure("WareHouse not found", 404);
				return response;
			}

			var mapping = mapper.Map<List<GetWareHouseDTO>>(getAllWareHouse);

			response.Success(mapping);

			return response;
		}

		public async Task<GenericResponseApi<bool>> UpdateWareHouse(UpdateWareHouseDTO updateWareHouse)
		{
			var response = new GenericResponseApi<bool>();

			var getWareHouseId = await unitOfWork.GetRepository<WareHouse>().GetById(updateWareHouse.Id);

			if (getWareHouseId == null)
			{
				response.Failure("Id not found");
				return response;
			}

			var mapping = mapper.Map(updateWareHouse, getWareHouseId);

			unitOfWork.GetRepository<WareHouse>().UpdateAsync(mapping);

			await unitOfWork.Commit();

			response.Success(true);
			return response;
		}
	}
}
