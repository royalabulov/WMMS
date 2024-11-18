using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WMMS.DAL.Context;
using WMMS.DAL.Repositories.EntityRepository;
using WMMS.Domain.RepositoryInterface;

namespace WMMS.DAL
{
	public static class DataAccessDependencyInjection
	{
		public static void DataAccessDependencyInjectionMethod(this IServiceCollection services,IConfiguration configuration)
		{
			services.AddDbContext<AppDBContext>(opt =>
			{
				opt.UseSqlServer(configuration.GetConnectionString("WMMS"));
			});

			services.AddScoped<IMarketRepository, MarketRepository>();
			services.AddScoped<IProductRepository, ProductRepository>();
			services.AddScoped<ISaleRepository,SaleRepository>();
			services.AddScoped<IStockTransferRepository,StockTransferRepository>();
			services.AddScoped<IWareHouseInventoryRepository,WareHouseInventoryRepository>();
			services.AddScoped<IWareHouseRepository, WareHouseRepository>();
			services.AddScoped<IMarketInventoryRepository, MarketInventoryRepository>();
		}


	}
}
