using WMMS.BLL.Model.DTO_s.ProductDTO_s;
using WMMS.BLL.Model.GenericResponseApi;

namespace WMMS.BLL.Services.Interface
{
	public interface IProductService
	{
		Task<GenericResponseApi<List<GetProductDTO>>> GetAllProduct();
		Task<GenericResponseApi<bool>> CreateProduct(CreateProductDTO createProduct);
		Task<GenericResponseApi<bool>> UpdateProduct(UpdateProductDTO updateProduct);
		Task<GenericResponseApi<bool>> DeleteProduct(int id);
	}
}
