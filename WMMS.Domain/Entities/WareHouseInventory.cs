namespace WMMS.Domain.Entities
{
	public class WareHouseInventory : BaseEntity
	{
		public int WareHouseQuantity { get; set; }
		public DateTime ArrivalDate { get; set; }


	    public int ProductId {  get; set; }
		public Product Product { get; set; }
		public int WareHouseId { get; set; }
		public WareHouse WareHouse { get; set; }
	}
}
