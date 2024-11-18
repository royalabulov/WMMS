namespace WMMS.Domain.Entities
{
	public class Sale : BaseEntity
	{
		public int Quantity { get; set; }
		public decimal TotalPrice { get; set; }
		public DateTime SaleDate { get; set; }

		public int MarketId {  get; set; }
		public Market Market { get; set; }

		public int ProductId {  get; set; }
		public Product Product { get; set; }
	}
}
