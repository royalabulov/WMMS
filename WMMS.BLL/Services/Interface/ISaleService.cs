using WMMS.BLL.Model.DTO_s.SaleDTO_s;
using WMMS.BLL.Model.GenericResponseApi;

namespace WMMS.BLL.Services.Interface
{
	public interface ISaleService
	{
		Task<GenericResponseApi<List<GetSaleSummaryResponseDTO>>> GetAllSale(int marketId, DateTime? startDate = null, DateTime? endDate = null);
		Task<GenericResponseApi<bool>> CreateSale(CreateSaleDTO createSale);
	}
}
