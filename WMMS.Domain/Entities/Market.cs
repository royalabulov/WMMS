
namespace WMMS.Domain.Entities
{
	public class Market : BaseEntity
	{
		public string Name { get; set; }
		public string Location { get; set; }


		public int AppUserId { get; set; }
		public AppUser AppUser { get; set; }

		public ICollection<StockTransfer> StockTransfer { get; set; }

		public ICollection<Sale> Sale { get; set; }

		public int WareHouseId { get; set; }
		public WareHouse WareHouse { get; set; }
	}
}
