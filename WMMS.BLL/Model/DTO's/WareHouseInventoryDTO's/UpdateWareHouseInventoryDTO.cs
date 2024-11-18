namespace WMMS.BLL.Model.DTO_s.WareHouseInventoryDTO_s
{
	public class UpdateWareHouseInventoryDTO
	{
		public int ProductId {  get; set; }
		public string ProductName { get; set; }
		public int WareHouseId{  get; set; }
		public int WareHouseQuantity {  get; set; }
		public decimal ProductPrice {  get; set; }
		public DateTime ArrivalDate { get; set; }
	}
}
