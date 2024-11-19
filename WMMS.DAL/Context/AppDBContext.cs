using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using WMMS.Domain.Entities;

namespace WMMS.DAL.Context
{
	public class AppDBContext(DbContextOptions<AppDBContext> options) : IdentityDbContext<AppUser, AppRole, int>(options)
	{
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);


			modelBuilder.Entity<Sale>()
			   .Property(s => s.TotalPrice)
			   .HasColumnType("decimal(18, 4)");

			modelBuilder.Entity<Product>()
			   .Property(p => p.Price)
			   .HasColumnType("decimal(18,4)");

			modelBuilder.Entity<MarketInventory>()
			   .Property(m => m.ProductPrice)
			   .HasColumnType("decimal(18,4)");


			modelBuilder.Entity<MarketInventory>()
				.HasOne(s => s.Product)
				.WithMany(p => p.MarketInventory)
				.HasForeignKey(s => s.ProductId)
				.OnDelete(DeleteBehavior.Restrict);


			// MarketInventory və Market arasındakı əlaqə
			modelBuilder.Entity<MarketInventory>()
				.HasOne(mi => mi.Market)
				.WithMany()
				.HasForeignKey(mi => mi.MarketId)
				.OnDelete(DeleteBehavior.Restrict);


			// Sale və Product arasındakı əlaqə
			modelBuilder.Entity<Sale>()
				.HasOne(s => s.Product)
				.WithMany(p => p.Sales)
				.HasForeignKey(s => s.ProductId)
				.OnDelete(DeleteBehavior.Restrict);



			// StockTransfer və Product arasındakı əlaqə
			modelBuilder.Entity<StockTransfer>()
				.HasOne(st => st.Product)
				.WithMany(p => p.StockTransfer)
				.HasForeignKey(st => st.ProductId)
				.OnDelete(DeleteBehavior.Restrict);

			// StockTransfer və WareHouse arasındakı əlaqə
			modelBuilder.Entity<StockTransfer>()
				.HasOne(st => st.WareHouse)
				.WithMany(w => w.StockTransfer)
				.HasForeignKey(st => st.WareHouseId)
				.OnDelete(DeleteBehavior.Restrict);

			// Market və AppUser arasındakı əlaqə
			modelBuilder.Entity<Market>()
				.HasOne(m => m.AppUser)
				.WithOne(u => u.Market)
				.HasForeignKey<Market>(m => m.AppUserId)
				.OnDelete(DeleteBehavior.Restrict);

			// Market və WareHouse arasındakı əlaqə
			modelBuilder.Entity<Market>()
				.HasOne(m => m.WareHouse)
				.WithOne(w => w.Market)
				.HasForeignKey<Market>(m => m.WareHouseId)
				.OnDelete(DeleteBehavior.Restrict);

			// AppUser və WareHouse arasındakı əlaqə
			modelBuilder.Entity<WareHouse>()
				.HasOne(w => w.User)
				.WithOne(u => u.WareHouse)
				.HasForeignKey<WareHouse>(w => w.AppUserId)
				.OnDelete(DeleteBehavior.Restrict);

			// Market üçün AppUserId indexi
			modelBuilder.Entity<Market>(entity =>
			{
				entity.HasIndex(e => e.AppUserId, "IX_Markets_AppUserId").IsUnique();
				entity.HasOne(d => d.AppUser)
					  .WithOne(p => p.Market)
					  .HasForeignKey<Market>(d => d.AppUserId);
			});
		}

		public DbSet<Market> Markets { get; set; }
		public DbSet<MarketInventory> MarketInventories { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<WareHouseInventory> WareHouseInventories { get; set; }
		public DbSet<Sale> Sales { get; set; }
		public DbSet<StockTransfer> StockTransfer { get; set; }
		public DbSet<WareHouse> WareHouse { get; set; }
	}
}
