using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WMMS.BLL.Model.DTO_s.SaleDTO_s;
using WMMS.BLL.Model.GenericResponseApi;
using WMMS.BLL.Services.Interface;
using WMMS.Domain.Entities;
using WMMS.Domain.UnitOfWorkInterface;

namespace WMMS.BLL.Services.Implementation
{
	public class SaleService : ISaleService
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper mapper;

		public SaleService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			this.unitOfWork = unitOfWork;
			this.mapper = mapper;
		}

		public async Task<GenericResponseApi<bool>> CreateSale(CreateSaleDTO createSaleDto)
		{
			var response = new GenericResponseApi<bool>();

			var market = await unitOfWork.GetRepository<Market>().FirstOrDefaultAsync(x => x.Id == createSaleDto.MarketId);

			if (market == null)
			{
				response.Failure("Market not found", 404);
				return response;
			}

			foreach (var products in createSaleDto.Products)
			{
				var product = await unitOfWork.GetRepository<Product>().FirstOrDefaultAsync(x => x.Id == products.ProductId);

				if (product == null)
				{
					response.Failure("Product with ID not found.", 404);
					return response;
				}

				var marketInventory = await unitOfWork
					.GetRepository<MarketInventory>()
					.FirstOrDefaultAsync(x => x.MarketId == createSaleDto.MarketId && x.ProductId == products.ProductId);

				if (marketInventory == null || marketInventory.ProductQuantity < products.Quantity)
				{
					response.Failure("Not enough quantity for product ");
					return response;
				}

				var mapping = mapper.Map<Sale>(products);
				mapping.MarketId = createSaleDto.MarketId;
				mapping.TotalPrice = products.Quantity * products.PricePerUnit;
				mapping.SaleDate = createSaleDto.SaleDate;


				await unitOfWork.GetRepository<Sale>().AddAsync(mapping);

				marketInventory.ProductQuantity -= products.Quantity;
				unitOfWork.GetRepository<MarketInventory>().UpdateAsync(marketInventory);

			}
			await unitOfWork.Commit();
			response.Success(true);
			return response;
		}

		public async Task<GenericResponseApi<List<GetSaleSummaryResponseDTO>>> GetAllSale(int marketId, DateTime? startDate = null, DateTime? endDate = null)
		{
			var response = new GenericResponseApi<List<GetSaleSummaryResponseDTO>>();

			if (startDate == null)
			{
				startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
			}

			if (endDate == null)
			{
				endDate = DateTime.Now;
			}

			var market = await unitOfWork.GetRepository<Market>().FirstOrDefaultAsync(x => x.Id == marketId);
			if (market == null)
			{
				response.Failure("Market not found", 404);
				return response;
			}

			var sales = await unitOfWork
				.GetRepository<Sale>()
				.GetAsQueryable()
				.Where(x => x.MarketId == marketId && x.SaleDate.Date >= startDate.Value.Date && x.SaleDate.Date <= endDate.Value.Date)
				.ToListAsync();

			if (sales == null || !sales.Any())
			{
				response.Failure("No sales found for this date range", 404);
				return response;
			}

			var summarySales = sales.GroupBy(x => x.SaleDate.ToString("yyyy-MM"))
				.OrderBy(x => x.Key)
				.Select(x => new GetSaleSummaryResponseDTO
				{
					Month = x.Key,
					DailySales = x.GroupBy(x => x.SaleDate.Date).Select(x => new GetSaleDTO
					{
						SaleDate = x.Key,
						ProductName = x.First().Product.ProductName,
						Quantity = x.Sum(x => x.Quantity),
						TotalPrice = x.Sum(x => x.Quantity * x.TotalPrice)

					}).OrderBy(x => x.SaleDate).ToList()

			}).ToList();


			var totalQuantity = summarySales.Sum(x => x.DailySales.Sum(s => s.Quantity));
			var totalPrice = summarySales.Sum(x => x.DailySales.Sum(s => s.TotalPrice));

			foreach (var month in summarySales)
			{
				month.TotalQuantity = totalQuantity;
				month.TotalPrice = totalPrice;
			}


			response.Success(summarySales);
			return response;
		}


	}
}
