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
		private readonly IMarketInventoryService marketInventoryService;

		public StockTransferService(IUnitOfWork unitOfWork, IMapper mapper, IMarketInventoryService marketInventoryService)
		{
			this.unitOfWork = unitOfWork;
			this.mapper = mapper;
			this.marketInventoryService = marketInventoryService;
		}

		public async Task<GenericResponseApi<bool>> CreateStockTransfer(CreateStockTransferDTO createStockTransfer)
		{
			var response = new GenericResponseApi<bool>();

			try
			{
				// Anbar məlumatını yoxlama
				var wareHouseInventory = await unitOfWork
					.GetRepository<WareHouseInventory>()
					.FirstOrDefaultAsync(x => x.ProductId == createStockTransfer.ProductId && x.WareHouseId == createStockTransfer.WareHouseId);

				if (wareHouseInventory == null || wareHouseInventory.WareHouseQuantity < createStockTransfer.Quantity)
				{
					response.Failure("Not enough products in stock.", 404);
					return response;
				}

				// Anbar miqdarını azaldırıq
				wareHouseInventory.WareHouseQuantity -= createStockTransfer.Quantity;
				unitOfWork.GetRepository<WareHouseInventory>().UpdateAsync(wareHouseInventory);

				// StockTransfer obyekti yaratmaq
				var stockTransfer = mapper.Map<StockTransfer>(createStockTransfer);
				await unitOfWork.GetRepository<StockTransfer>().AddAsync(stockTransfer);

				// MarketInventory-ə əlavə etmək
				await marketInventoryService.AddOrUpdateMarketInventoryAsync(stockTransfer);

				// Dəyişiklikləri yaddaşa saxlamaq
				await unitOfWork.Commit();

				// Müvəffəqiyyətli cavab
				response.Success(true);
				return response;
			}
			catch (Exception ex)
			{
				// Xəta baş verdikdə, müvafiq olaraq cavab qaytarırıq
				response.Failure($"An error occurred: {ex.Message}", 500);
				return response;
			}

		}

		public async Task<GenericResponseApi<List<List<GetWareHouseTransferDTO>>>> GetWareHouseTransfer(int wareHouseId)
		{
			var response = new GenericResponseApi<List<List<GetWareHouseTransferDTO>>>();
			try
			{
				var transfers = await unitOfWork
					.GetRepository<StockTransfer>()
					.GetAsQueryable().Include(x => x.Product)
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

			}
			catch (Exception ex)
			{
				response.Failure(ex.Message, 500);
			}
			return response;
		}

		public async Task<GenericResponseApi<List<List<GetMarketTransferDTO>>>> GetMarketTransfer(int marketId)
		{
			var response = new GenericResponseApi<List<List<GetMarketTransferDTO>>>();
			try
			{
				var transfers = await unitOfWork
					.GetRepository<StockTransfer>()
					.GetAsQueryable().Include(x => x.Product)
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
			}
			catch (Exception ex)
			{
				response.Failure(ex.Message, 500);
			}

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

			int oldQuantity = stockTransfer.Quantity;
			int newQuantity = updateStockTransfer.Quantity;


			var mapping = mapper.Map(updateStockTransfer, stockTransfer);

			unitOfWork.GetRepository<StockTransfer>().UpdateAsync(mapping);

			var wareHouseInventory = await unitOfWork
				.GetRepository<WareHouseInventory>()
				.FirstOrDefaultAsync(x => x.ProductId == stockTransfer.ProductId && x.WareHouseId == stockTransfer.WareHouseId);

			if (wareHouseInventory == null)
			{
				response.Failure("Warehouse inventory not found.", 404);
				return response;
			}

			if (newQuantity < oldQuantity)
			{
				// Miqdar azalıbsa, WarehouseInventory-ə həmin miqdarı geri qaytarırıq
				wareHouseInventory.WareHouseQuantity += (oldQuantity - newQuantity); // Yəni, fərq qədər geri qaytarılır
			}
			else if (newQuantity > oldQuantity)
			{
				if (wareHouseInventory.WareHouseQuantity < (newQuantity - oldQuantity))
				{
					response.Failure("Product quantity not found in warehouse for transfer.", 404);
					return response;
				}
				wareHouseInventory.WareHouseQuantity -= (newQuantity - oldQuantity);
			}



			unitOfWork.GetRepository<WareHouseInventory>().UpdateAsync(wareHouseInventory);

			await unitOfWork.Commit();
			response.Success(true);
			return response;
		}

	}
}
