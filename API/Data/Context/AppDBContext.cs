


using Microsoft.EntityFrameworkCore;

public class AppDBContext : DbContext
{



      public AppDBContext(DbContextOptions options) : base(options)
      {

      }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDBContext).Assembly);


      }
}
