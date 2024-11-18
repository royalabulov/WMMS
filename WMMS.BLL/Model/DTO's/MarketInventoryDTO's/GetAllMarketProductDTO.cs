namespace WMMS.BLL.Model.DTO_s.MarketInventoryDTO_s
{
	public class GetAllMarketProductDTO
	{
		public int Id { get; set; }
		public int MarketId {  get; set; }
		public string ProductName { get; set; }
		public decimal ProductPrice {  get; set; }
		public int ProductQuantity {  get; set; }
		public DateTime ArrivalDate { get; set; }
	}
}
