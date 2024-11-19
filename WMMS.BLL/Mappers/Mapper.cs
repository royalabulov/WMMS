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
			CreateMap<Market, CreateMarketDTO>()
				.ForMember(x => x.WareHouseId, opt => opt.MapFrom(mf => mf.WareHouseId)).ReverseMap();


			//WareHouseInventory
			CreateMap<WareHouseInventory, GetAllWareHouseProductDTO>()
				.ForMember(x => x.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
				.ForMember(x => x.ProductPrice, opt => opt.MapFrom(opt => opt.Product.Price)).ReverseMap();
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
			CreateMap<CreateStockTransferDTO, StockTransfer>()
			   .ForMember(x => x.ProductName, opt => opt.MapFrom(src => src.ProductName))
			   .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
			   .ForMember(dest => dest.WareHouseId, opt => opt.MapFrom(src => src.WareHouseId))
			   .ForMember(dest => dest.MarketId, opt => opt.MapFrom(src => src.MarketId))
			   .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
			   .ForMember(dest => dest.TransferDate, opt => opt.MapFrom(src => src.TransferDate));



			CreateMap<StockTransfer, MarketInventory>()
			   .ForMember(dest => dest.MarketId, opt => opt.MapFrom(src => src.MarketId))
			   .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
			   .ForMember(dest => dest.ProductQuantity, opt => opt.MapFrom(src => src.Quantity))
			   .ForMember(dest => dest.ArrivalDate, opt => opt.MapFrom(src => src.TransferDate));


			CreateMap<StockTransfer, GetWareHouseTransferDTO>()
			   .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
			   .ForMember(dest => dest.TransferDate, opt => opt.MapFrom(src => src.TransferDate));


			CreateMap<StockTransfer, GetMarketTransferDTO>()
			   .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
			   .ForMember(dest => dest.TransferDate, opt => opt.MapFrom(src => src.TransferDate));

			CreateMap<StockTransfer, UpdateStockTransfer>().ReverseMap();


			//MarketInventory
			CreateMap<MarketInventory, GetAllMarketProductDTO>()
		  .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.Price));

			CreateMap<MarketInventory, UpdateMarketProductDTO>().ReverseMap();


			//Product
			CreateMap<Product, GetProductDTO>().ReverseMap();
			CreateMap<Product, CreateProductDTO>().ReverseMap();
			CreateMap<Product, UpdateProductDTO>().ReverseMap();
		}
	}
}
