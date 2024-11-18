using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WMMS.BLL.Model.DTO_s.MarketInventoryDTO_s;
using WMMS.BLL.Model.GenericResponseApi;
using WMMS.BLL.Services.Interface;
using WMMS.Domain.Entities;
using WMMS.Domain.UnitOfWorkInterface;

namespace WMMS.BLL.Services.Implementation
{
	public class MarketInventoryService : IMarketInventoryService
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper mapper;

		public MarketInventoryService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			this.unitOfWork = unitOfWork;
			this.mapper = mapper;
		}

		public async Task<GenericResponseApi<List<GetAllMarketProductDTO>>> AllMarketProduct(int marketId)
		{
			var response = new GenericResponseApi<List<GetAllMarketProductDTO>>();

			var allProducts = await unitOfWork
				.GetRepository<MarketInventory>()
				.GetAsQueryable()
				.Where(x => x.MarketId == marketId)
				.ToListAsync();

			var mapping = mapper.Map<List<GetAllMarketProductDTO>>(allProducts);
			response.Success(mapping);
			return response;

		}

		public async Task<GenericResponseApi<bool>> CreateMarketInventory(CreateMarketProductDTO createMarketProductDTO)
		{
			var response = new GenericResponseApi<bool>();

			var mapping = mapper.Map<MarketInventory>(createMarketProductDTO);

			var existingProduct = await unitOfWork
				.GetRepository<MarketInventory>()
				.FirstOrDefaultAsync(x => x.MarketId == mapping.MarketId && x.ProductId == mapping.ProductId);

			if (existingProduct != null)
			{
				existingProduct.ProductQuantity += mapping.ProductQuantity;
				existingProduct.ArrivalDate = mapping.ArrivalDate;
				unitOfWork.GetRepository<MarketInventory>().UpdateAsync(existingProduct);
			}
			else
			{
				await unitOfWork.GetRepository<MarketInventory>().AddAsync(mapping);
			}

			await unitOfWork.Commit();
			response.Success(true);
			return response;

		}

		public async Task<GenericResponseApi<bool>> DeleteProduct(int productId, int marketId)
		{
			var response = new GenericResponseApi<bool>();

			var getProductId = await unitOfWork
				.GetRepository<MarketInventory>()
				.FirstOrDefaultAsync(x => x.ProductId == productId && x.MarketId == marketId);

			if (getProductId == null)
			{
				response.Failure("Product not found", 404);
				return response;
			}
			unitOfWork.GetRepository<MarketInventory>().DeleteAsync(getProductId);
			await unitOfWork.Commit();
			response.Success(true);
			return response;
		}

		public async Task<GenericResponseApi<bool>> UpdateMarketProduct(UpdateMarketProductDTO updateMarketProductDTO)
		{
			var response = new GenericResponseApi<bool>();

			var getProductId = await unitOfWork
				.GetRepository<MarketInventory>()
				.FirstOrDefaultAsync(x => x.MarketId == updateMarketProductDTO.MarketId && x.ProductId == updateMarketProductDTO.ProductId);

			var mapping = mapper.Map(updateMarketProductDTO, getProductId);

			unitOfWork.GetRepository<MarketInventory>().UpdateAsync(mapping);
			await unitOfWork.Commit();
			response.Success(true);
			return response;
		}
	}
}
