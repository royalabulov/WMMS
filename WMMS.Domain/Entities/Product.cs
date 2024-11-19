namespace WMMS.Domain.Entities
{
	public class Product : BaseEntity
	{
		public string ProductName { get; set; }
		public decimal Price { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime ExpireDate { get; set; }



		public ICollection<Sale> Sales { get; set; }
		public ICollection<WareHouseInventory> WareHouseInventories { get; set; }
		public ICollection<StockTransfer> StockTransfer { get; set; }
		public ICollection<MarketInventory> MarketInventory { get; set; }
	}

	
}
