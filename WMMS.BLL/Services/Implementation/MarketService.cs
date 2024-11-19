using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WMMS.BLL.Model.DTO_s.MarketDTO_s;
using WMMS.BLL.Model.GenericResponseApi;
using WMMS.BLL.Services.Interface;
using WMMS.Domain.Entities;
using WMMS.Domain.UnitOfWorkInterface;

namespace WMMS.BLL.Services.Implementation
{
	public class MarketService : IMarketService
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IHttpContextAccessor httpContextAccessor;
		private readonly IMapper mapper;

		public MarketService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper)
		{
			this.unitOfWork = unitOfWork;
			this.httpContextAccessor = httpContextAccessor;
			this.mapper = mapper;
		}

		public async Task<GenericResponseApi<bool>> CreateMarket(CreateMarketDTO createMarketDTO)
		{
			var response = new GenericResponseApi<bool>();
			try
			{
				var warehouse = await unitOfWork
				   .GetRepository<WareHouse>()
				   .GetAsQueryable()
				   .FirstOrDefaultAsync(x => x.Id == createMarketDTO.WareHouseId);

				if (warehouse == null)
				{
					response.Failure("Bu Warehouse ID ilə əlaqəli bir WareHouse tapılmadı.", 404);
					return response;
				}


				var mapping = mapper.Map<Market>(createMarketDTO);

				var currentMarket = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

				if (currentMarket == null)
				{
					response.Failure("currentMarket not found", 404);
					return response;
				}

				mapping.AppUserId = int.Parse(currentMarket);

				await unitOfWork.GetRepository<Market>().AddAsync(mapping);
				await unitOfWork.Commit();

			}
			catch (Exception ex)
			{
				response.Failure($"error {ex.Message}", 500);
			}
			response.Success(true);
			return response;
		}

		public async Task<GenericResponseApi<bool>> DeleteMarket(int id)
		{
			var response = new GenericResponseApi<bool>();

			var getMarketId = await unitOfWork.GetRepository<Market>().GetById(id);

			if (getMarketId == null)
			{
				response.Failure("Id not found", 404);
				return response;
			}

			unitOfWork.GetRepository<Market>().DeleteAsync(getMarketId);
			await unitOfWork.Commit();
			response.Success(true);
			return response;
		}

		public async Task<GenericResponseApi<List<GetMarketDTO>>> GetAllMarket()
		{
			var response = new GenericResponseApi<List<GetMarketDTO>>();

			var getMarket = await unitOfWork.GetRepository<Market>().GetAll();

			if (getMarket == null)
			{
				response.Failure("Market not found", 404);
				return response;
			}

			var mapping = mapper.Map<List<GetMarketDTO>>(getMarket);

			response.Success(mapping);
			return response;
		}

		public async Task<GenericResponseApi<bool>> UpdateMarket(UpdateMarketDTO updateMarket)
		{
			var response = new GenericResponseApi<bool>();

			var getMarketId = await unitOfWork.GetRepository<Market>().GetById(updateMarket.Id);

			if (getMarketId == null)
			{
				response.Failure("Id not found", 404);
				return response;
			}
			var mapping = mapper.Map(updateMarket, getMarketId);

			await unitOfWork.Commit();
			response.Success(true);
			return response;
		}
	}
}
