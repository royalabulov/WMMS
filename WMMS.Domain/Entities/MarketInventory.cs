namespace WMMS.Domain.Entities
{
	public class MarketInventory : BaseEntity
	{
		public string ProductName { get; set; }
		public int ProductQuantity { get; set; }
		public decimal ProductPrice { get; set; }
		public DateTime ArrivalDate { get; set; }

		public int ProductId { get; set; }
		public Product Product { get; set; }

		public int MarketId { get; set; }
		public Market Market { get; set; }
	}
}
