using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WMMS.BLL.Model.DTO_s.WareHouseInventoryDTO_s;
using WMMS.BLL.Model.GenericResponseApi;
using WMMS.BLL.Services.Interface;
using WMMS.Domain.Entities;
using WMMS.Domain.UnitOfWorkInterface;

namespace WMMS.BLL.Services.Implementation
{
	public class WareHouseInventoryService : IWareHouseInventoryService
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper mapper;

		public WareHouseInventoryService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			this.unitOfWork = unitOfWork;
			this.mapper = mapper;
		}
		public async Task<GenericResponseApi<bool>> CreateWareHouseProduct(CreateWareHouseProductDTO createWareHouseProductDTO)
		{
			var response = new GenericResponseApi<bool>();

			var mapping = mapper.Map<WareHouseInventory>(createWareHouseProductDTO);
			
			var existingProduct = await unitOfWork
				.GetRepository<WareHouseInventory>()
				.FirstOrDefaultAsync(x => x.WareHouseId == mapping.WareHouseId && x.ProductId == mapping.ProductId);

			if (existingProduct != null)
			{
				existingProduct.WareHouseQuantity += mapping.WareHouseQuantity;
				existingProduct.ArrivalDate = mapping.ArrivalDate;

				unitOfWork.GetRepository<WareHouseInventory>().UpdateAsync(existingProduct);
			}
			else
			{
				await unitOfWork.GetRepository<WareHouseInventory>().AddAsync(mapping);
			}

			await unitOfWork.Commit();
			response.Success(true);
			return response;
		}

		public async Task<GenericResponseApi<List<GetAllWareHouseProductDTO>>> GetAllWareHouseProduct(int wareHouseId)
		{
			var response = new GenericResponseApi<List<GetAllWareHouseProductDTO>>();

			var allProducts = await unitOfWork
				.GetRepository<WareHouseInventory>()
				.GetAsQueryable()
				.Where(x => x.WareHouseId == wareHouseId)
				.ToListAsync();

			var mapping = mapper.Map<List<GetAllWareHouseProductDTO>>(allProducts);

			response.Success(mapping);
			return response;
		}  

		public async Task<GenericResponseApi<bool>> RemoveProduct(int ProductId, int WareHouseId)
		{
			var response = new GenericResponseApi<bool>();

			var getProductId = await unitOfWork
				.GetRepository<WareHouseInventory>()
				.FirstOrDefaultAsync(x => x.ProductId == ProductId && x.WareHouseId == WareHouseId);

			unitOfWork.GetRepository<WareHouseInventory>().DeleteAsync(getProductId);

			await unitOfWork.Commit();
			response.Success(true);
			return response;

		}

		public async Task<GenericResponseApi<bool>> UpdateWareHouseProduct(UpdateWareHouseInventoryDTO updateWareHouseProductDTO)
		{
			var response = new GenericResponseApi<bool>();

			var getProduct = await unitOfWork
				.GetRepository<WareHouseInventory>()
				.FirstOrDefaultAsync(x => x.ProductId == updateWareHouseProductDTO.ProductId && x.WareHouseId == updateWareHouseProductDTO.WareHouseId);
			    
			if(getProduct == null)
			{
				response.Failure("Product not found", 404);
				return response;
			}


			var mapping = mapper.Map(updateWareHouseProductDTO,getProduct);

			unitOfWork.GetRepository<WareHouseInventory>().UpdateAsync(mapping);
			await unitOfWork.Commit();
			response.Success(true);
			return response;
		}
	}
}
