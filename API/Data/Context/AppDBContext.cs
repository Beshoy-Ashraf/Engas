


using API.Data.Models.Staff;
using API.Data.Models.Store;
using Microsoft.EntityFrameworkCore;

public class AppDBContext(DbContextOptions options) : DbContext(options)
{
      public DbSet<Store> Stores { get; set; }
      public DbSet<Staff> Staffs { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDBContext).Assembly);


      }
}
