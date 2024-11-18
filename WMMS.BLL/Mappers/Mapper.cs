using AutoMapper;
using WMMS.BLL.Model.DTO_s.MarketDTO_s;
using WMMS.BLL.Model.DTO_s.MarketInventoryDTO_s;
using WMMS.BLL.Model.DTO_s.ProductDTO_s;
using WMMS.BLL.Model.DTO_s.RegisterDTO_s;
using WMMS.BLL.Model.DTO_s.RoleDTO_s;
using WMMS.BLL.Model.DTO_s.StockTransferDTO_s;
using WMMS.BLL.Model.DTO_s.WareHouseDTO_s;
using WMMS.BLL.Model.DTO_s.WareHouseInventoryDTO_s;
using WMMS.Domain.Entities;

namespace WMMS.BLL.Mappers
{
	public class Mapper : Profile
	{
		//MAPPERDE PROBLEM OLA BILER
		public Mapper()
		{
			//Role
			CreateMap<AppRole, GetRoleDTO>().ReverseMap();
			CreateMap<AppRole, UpdateRoleDTO>().ReverseMap();


			//UserWareHouse
			CreateMap<CreateStorekeeperDTO, AppUser>().ForMember(fm => fm.UserName, opt => opt.MapFrom(mf => mf.Email)).ReverseMap();
			CreateMap<CreateMarketManagerDTO, AppUser>().ForMember(fm => fm.UserName, opt => opt.MapFrom(mf => mf.Email)).ReverseMap();
			CreateMap<AppUser, UserUpdateDTO>().ReverseMap();
			CreateMap<AppUser, GetAllUserDTO>().ReverseMap();


			//WareHouse
			CreateMap<WareHouse, GetWareHouseDTO>().ReverseMap();
			CreateMap<WareHouse, UpdateWareHouseDTO>().ReverseMap();
			CreateMap<WareHouse, CreateWareHouseDTO>().ReverseMap();


			//Market
			CreateMap<Market, GetMarketDTO>().ReverseMap();
			CreateMap<Market, UpdateMarketDTO>().ReverseMap();
			CreateMap<Market, CreateMarketDTO>().ReverseMap();


			//WareHouseInventory
			CreateMap<WareHouseInventory, GetAllWareHouseProductDTO>().ReverseMap();
			CreateMap<WareHouseInventory, CreateWareHouseProductDTO>().ReverseMap();
			CreateMap<WareHouseInventory, UpdateWareHouseInventoryDTO>().ReverseMap();


			//MarketInventory
			CreateMap<CreateStockTransferDTO, MarketInventory>()
				.ForMember(x => x.ProductId, opt => opt.MapFrom(src => src.ProductId))
				.ForMember(x => x.MarketId, opt => opt.MapFrom(src => src.MarketId))
				.ForMember(x => x.ProductQuantity, opt => opt.MapFrom(src => src.Quantity))
				.ForMember(x => x.ArrivalDate, opt => opt.MapFrom(src => src.TransferDate)).ReverseMap();
			CreateMap<MarketInventory, GetAllMarketProductDTO>().ReverseMap();
			CreateMap<MarketInventory, UpdateMarketProductDTO>().ReverseMap();


			//StockTransfer
			CreateMap<StockTransfer, CreateStockTransferDTO>()
				.ForMember(x => x.MarketId, opt => opt.MapFrom(src => src.MarketId))
				.ForMember(x => x.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
				.ForMember(x => x.ProductId, opt => opt.MapFrom(src => src.ProductId))
				.ForMember(x => x.WarehouseId, opt => opt.MapFrom(src => src.WareHouseId))
				.ForMember(x => x.Quantity, opt => opt.MapFrom(src => src.Quantity))
				.ForMember(x => x.TransferDate, opt => opt.MapFrom(opt => opt.TransferDate)).ReverseMap();

			CreateMap<StockTransfer, GetAllWareHouseProductDTO>()
				.ForMember(x => x.ProductName, opt => opt.MapFrom(src => src.Product.ProductName)).ReverseMap(); //xeta ola biler

			CreateMap<StockTransfer, UpdateStockTransfer>().ReverseMap();


			//MarletInventory
			CreateMap<CreateMarketProductDTO, MarketInventory>()
				.ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
				.ForMember(dest => dest.ProductQuantity, opt => opt.MapFrom(src => src.ProductQuantity))
				.ForMember(dest => dest.ArrivalDate, opt => opt.MapFrom(src => src.ArrivalDate))
				.ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
				.ForMember(dest => dest.MarketId, opt => opt.MapFrom(src => src.MarketId)).ReverseMap();

			CreateMap<MarketInventory, GetAllMarketProductDTO>().ReverseMap();
			CreateMap<MarketInventory, UpdateMarketProductDTO>().ReverseMap();


			//Product
			CreateMap<Product, GetProductDTO>().ReverseMap();
			CreateMap<Product, CreateProductDTO>().ReverseMap();
			CreateMap<Product, UpdateProductDTO>().ReverseMap();
		}
	}
}
