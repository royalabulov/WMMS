namespace WMMS.BLL.Model.DTO_s.MarketInventoryDTO_s
{
	public class CreateMarketProductDTO
	{
		public int MarketId { get; set; }
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public decimal ProductPrice { get; set; }
		public int ProductQuantity { get; set; }
		public DateTime ArrivalDate { get; set; }
	}
}
