using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WMMS.BLL.Model.DTO_s.StockTransferDTO_s;
using WMMS.BLL.Model.GenericResponseApi;
using WMMS.BLL.Services.Interface;
using WMMS.Domain.Entities;
using WMMS.Domain.UnitOfWorkInterface;

namespace WMMS.BLL.Services.Implementation
{
	public class StockTransferService : IStockTransferService
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper mapper;

		public StockTransferService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			this.unitOfWork = unitOfWork;
			this.mapper = mapper;
		}

		public async Task<GenericResponseApi<bool>> CreateStockTransfer(CreateStockTransferDTO createStockTransfer)
		{
			var response = new GenericResponseApi<bool>();

			var wareHouseInventory = await unitOfWork
				.GetRepository<WareHouseInventory>()
				.FirstOrDefaultAsync(x => x.ProductId == createStockTransfer.ProductId && x.WareHouseId == createStockTransfer.WarehouseId);

			if (wareHouseInventory == null || wareHouseInventory.WareHouseQuantity < createStockTransfer.Quantity)
			{
				response.Failure("Not enough products in stock.", 404);
				return response;
			}

			var marketInventory = await unitOfWork
				.GetRepository<MarketInventory>()
				.FirstOrDefaultAsync(x => x.ProductId == createStockTransfer.ProductId && x.MarketId == createStockTransfer.MarketId);

			if (marketInventory != null)
			{
				marketInventory.ProductQuantity += createStockTransfer.Quantity;
				unitOfWork.GetRepository<MarketInventory>().UpdateAsync(marketInventory);
			}
			else
			{
				var mapping = mapper.Map<MarketInventory>(createStockTransfer);
				await unitOfWork.GetRepository<MarketInventory>().AddAsync(mapping);
			}

			wareHouseInventory.WareHouseQuantity -= createStockTransfer.Quantity;
			unitOfWork.GetRepository<WareHouseInventory>().UpdateAsync(wareHouseInventory);

			var stockTransfer = mapper.Map<StockTransfer>(createStockTransfer);
			await unitOfWork.GetRepository<StockTransfer>().AddAsync(stockTransfer);

			await unitOfWork.Commit();
			response.Success(true);
			return response;

		}

		public async Task<GenericResponseApi<List<List<GetWareHouseTransferDTO>>>> GetWareHouseTransfer(int wareHouseId)
		{
			var response = new GenericResponseApi<List<List<GetWareHouseTransferDTO>>>();

			var transfers = await unitOfWork
				.GetRepository<StockTransfer>()
				.GetAsQueryable()
				.Where(x => x.WareHouseId == wareHouseId)
				.OrderBy(x => x.TransferDate)
				.ToListAsync();

			if (!transfers.Any())
			{
				response.Failure("No transfers found for this product in the specified warehouse.");
				return response;
			}

			var result = new List<List<GetWareHouseTransferDTO>>();
			foreach (var transfer in transfers)
			{
				var transferGroup = new List<GetWareHouseTransferDTO>
				{

					mapper.Map<GetWareHouseTransferDTO>(transfer)
				};

				result.Add(transferGroup);
			}

			response.Success(result);
			return response;
		}

		public async Task<GenericResponseApi<List<List<GetMarketTransferDTO>>>> GetMarketTransfer(int marketId)
		{
			var response = new GenericResponseApi<List<List<GetMarketTransferDTO>>>();

			var transfers = await unitOfWork
				.GetRepository<StockTransfer>()
				.GetAsQueryable()
				.Where(x => x.MarketId == marketId)
				.OrderBy(x => x.TransferDate)
				.ToListAsync();

			if (!transfers.Any())
			{
				response.Failure("No transfers found for this product in the specified market.");
				return response;
			}

			var result = new List<List<GetMarketTransferDTO>>();
			foreach (var transfer in transfers)
			{
				var transferGroup = new List<GetMarketTransferDTO>
				{
					mapper.Map<GetMarketTransferDTO>(transfer)
				};
				result.Add(transferGroup);
			}

			response.Success(result);
			return response;
		}


		public async Task<GenericResponseApi<bool>> DeleteStockTransfer(int id)
		{
			var response = new GenericResponseApi<bool>();

			var stockTransfer = await unitOfWork.GetRepository<StockTransfer>().FirstOrDefaultAsync(x => x.Id == id);

			if (stockTransfer == null)
			{
				response.Failure("Transfer not found", 404);
				return response;
			}

			unitOfWork.GetRepository<StockTransfer>().DeleteAsync(stockTransfer);

			await unitOfWork.Commit();
			response.Success(true);
			return response;
		}

		public async Task<GenericResponseApi<bool>> UpdateStockTransfer(UpdateStockTransfer updateStockTransfer)
		{
			var response = new GenericResponseApi<bool>();

			var stockTransfer = await unitOfWork.GetRepository<StockTransfer>().FirstOrDefaultAsync(x => x.Id == updateStockTransfer.Id);

			if (stockTransfer == null)
			{
				response.Failure("Transfer not found", 404);
				return response;
			}

			var mapping = mapper.Map(updateStockTransfer, stockTransfer);

			unitOfWork.GetRepository<StockTransfer>().UpdateAsync(mapping);
			await unitOfWork.Commit();
			response.Success(true);
			return response;
		}

	}
}
