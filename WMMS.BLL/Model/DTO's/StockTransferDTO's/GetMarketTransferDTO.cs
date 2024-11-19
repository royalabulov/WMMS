namespace WMMS.BLL.Model.DTO_s.StockTransferDTO_s
{
	public class GetMarketTransferDTO
	{
		public int Id { get; set; }
		public string ProductName { get; set; }
		public int Quantity { get; set; }
		public DateTime TransferDate { get; set; }
	}
}
