using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WMMS.BLL.Model.DTO_s.ProductDTO_s;
using WMMS.BLL.Services.Interface;

namespace WarehouseMarketManagementSystem_WMMS_.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductService productService;

		public ProductController(IProductService productService)
		{
			this.productService = productService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllProduct()
		{
			var result = await productService.GetAllProduct();
			return StatusCode(result.StatusCode, result);
		}

		[HttpPost]
		public async Task<IActionResult> CreateProduct(CreateProductDTO createProduct)
		{
			var reult = await productService.CreateProduct(createProduct);
			return StatusCode(reult.StatusCode, reult);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateProduct(UpdateProductDTO updateProduct)
		{
			var result = await productService.UpdateProduct(updateProduct);
			return StatusCode(result.StatusCode, result);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			var result = await productService.DeleteProduct(id);
			return StatusCode(result.StatusCode, result);
		}
	}
}
