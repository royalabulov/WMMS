namespace WMMS.BLL.Model.DTO_s.StockTransferDTO_s
{
	public class CreateStockTransferDTO
	{
		public string ProductName { get; set; }
		public int Quantity { get; set; }
		public DateTime TransferDate { get; set; }
		public int ProductId { get; set; }
		public int MarketId { get; set; }
		public int WarehouseId { get; set; }

	}
}
