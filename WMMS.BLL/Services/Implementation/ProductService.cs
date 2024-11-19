using AutoMapper;
using WMMS.BLL.Model.DTO_s.ProductDTO_s;
using WMMS.BLL.Model.GenericResponseApi;
using WMMS.BLL.Services.Interface;
using WMMS.Domain.Entities;
using WMMS.Domain.UnitOfWorkInterface;

namespace WMMS.BLL.Services.Implementation
{
	public class ProductService : IProductService
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper mapper;

		public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			this.unitOfWork = unitOfWork;
			this.mapper = mapper;
		}

		public async Task<GenericResponseApi<bool>> CreateProduct(CreateProductDTO createProductDto)
		{
			var response = new GenericResponseApi<bool>();

			try
			{
				var mapping = mapper.Map<Product>(createProductDto);
				await unitOfWork.GetRepository<Product>().AddAsync(mapping);
				await unitOfWork.Commit();
				response.Success(true);
			}
			catch (Exception ex)
			{

				response.Failure($"Error: {ex.Message}", 500);
			}

			return response;

		}

		public async Task<GenericResponseApi<bool>> DeleteProduct(int id)
		{
			var response = new GenericResponseApi<bool>();

			var getProductId = await unitOfWork.GetRepository<Product>().GetById(id);

			if (getProductId == null)
			{
				response.Failure("Product not found");
				return response;
			}

			unitOfWork.GetRepository<Product>().DeleteAsync(getProductId);
			await unitOfWork.Commit();
			response.Success(true);
			return response;
		}

		public async Task<GenericResponseApi<List<GetProductDTO>>> GetAllProduct()
		{
			var response = new GenericResponseApi<List<GetProductDTO>>();

			var getAllProduct = await unitOfWork.GetRepository<Product>().GetAll();

			if (getAllProduct == null)
			{
				response.Failure("Product not found", 404);
				return response;
			}

			var mapping = mapper.Map<List<GetProductDTO>>(getAllProduct);
			response.Success(mapping);
			return response;
		}

		public async Task<GenericResponseApi<bool>> UpdateProduct(UpdateProductDTO updateProduct)
		{
			var response = new GenericResponseApi<bool>();

			var getProductId = await unitOfWork.GetRepository<Product>().GetById(updateProduct.Id);
			if (getProductId == null)
			{
				response.Failure("Id not found", 404);
				return response;
			}

			var mapping = mapper.Map(updateProduct, getProductId);

			unitOfWork.GetRepository<Product>().UpdateAsync(mapping);
			await unitOfWork.Commit();
			response.Success(true);
			return response;
		}
	}
}
