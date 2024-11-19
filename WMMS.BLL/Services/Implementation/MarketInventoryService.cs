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

		//Mehsul vay-yoxdu onu yoxluyuram
		public async Task<MarketInventory> GetMarketInventoryAsync(int marketId, int productId)
		{
			return await unitOfWork.GetRepository<MarketInventory>()
				.FirstOrDefaultAsync(x => x.MarketId == marketId && x.ProductId == productId);
		}

		//StockTransfer de caqirilacaq method
		public async Task AddOrUpdateMarketInventoryAsync(StockTransfer stockTransfer)
		{

			if (stockTransfer.MarketId == 0 || stockTransfer.ProductId == 0 || stockTransfer.Quantity <= 0)
			{
				throw new ArgumentException("MarketId, ProductId and Quantity must be provided and valid.");
			}

			var marketinventory = await GetMarketInventoryAsync(stockTransfer.MarketId, stockTransfer.ProductId);

			if (marketinventory != null)
			{
				marketinventory.ProductQuantity += stockTransfer.Quantity;
				marketinventory.ArrivalDate = stockTransfer.TransferDate;

				unitOfWork.GetRepository<MarketInventory>().UpdateAsync(marketinventory);
			}
			else
			{
				var mapping = mapper.Map<MarketInventory>(stockTransfer);

				if (mapping.MarketId == 0 || mapping.ProductId == 0 || mapping.ProductQuantity <= 0)
				{
					throw new ArgumentException("Invalid data when mapping to MarketInventory.");
				}

				await unitOfWork.GetRepository<MarketInventory>().AddAsync(mapping);

			}
			await unitOfWork.Commit();

		}


		public async Task<GenericResponseApi<List<GetAllMarketProductDTO>>> AllMarketProduct(int marketId)
		{
			var response = new GenericResponseApi<List<GetAllMarketProductDTO>>();

			var allProducts = await unitOfWork
				.GetRepository<MarketInventory>()
				.GetAsQueryable().Include(x => x.Product)
				.Where(x => x.MarketId == marketId)
				.ToListAsync();

			var mapping = mapper.Map<List<GetAllMarketProductDTO>>(allProducts);
			response.Success(mapping);
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
