using Microsoft.Extensions.DependencyInjection;
using WMMS.BLL.Mappers;
using WMMS.BLL.Services.Implementation;
using WMMS.BLL.Services.Interface;
using WMMS.DAL.UnitOfWorkImplmentation;
using WMMS.Domain.UnitOfWorkInterface;

namespace WMMS.BLL
{
	public static class BusinessLogicDependencyInjection
	{
		public static void BusinessLogicInjection(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(IMapperNavigate));

			services.AddScoped<IAppRoleService, AppRoleService>();
			services.AddScoped<IMarketService, MarketService>();
			services.AddScoped<IProductService, ProductService>();
			services.AddScoped<IRegisterService, RegisterService>();
			services.AddScoped<ISaleService, SaleService>();
			services.AddScoped<IStockTransferService, StockTransferService>();
			services.AddScoped<IWareHouseService, WareHouseService>();
			services.AddScoped<IWareHouseInventoryService, WareHouseInventoryService>();
			services.AddScoped<IMarketInventoryService, MarketInventoryService>();
			services.AddScoped<ILoginService, LoginService>();
			services.AddScoped<ITokenService, TokenService>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
		} 
	}
}
