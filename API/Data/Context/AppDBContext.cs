


using API.Data.Models.Item;
using API.Data.Models.Staff;
using API.Data.Models.Store;
using API.Data.Models.StoreStock;
using Microsoft.EntityFrameworkCore;

public class AppDBContext(DbContextOptions options) : DbContext(options)
{
      public DbSet<Store> Stores { get; set; }
      public DbSet<Staff> Staffs { get; set; }
      public DbSet<Item> Items { get; set; }
      public DbSet<StoreStockLevel> StoreStockLevels { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDBContext).Assembly);


      }
}
