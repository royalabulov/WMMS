namespace WMMS.BLL.Model.DTO_s.MarketInventoryDTO_s
{
	public class UpdateMarketProductDTO
	{
		public string ProductName { get; set; }
		public int MarketId { get; set; }
		public int ProductId { get; set; }
		public int ProductQuantity { get; set; }
		public decimal ProductPrice { get; set; }
		public DateTime ArrivalDate { get; set; }
	}
}
