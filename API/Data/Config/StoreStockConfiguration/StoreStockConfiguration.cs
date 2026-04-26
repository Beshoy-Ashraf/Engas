using API.Data.Models.StoreStock;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Data.Config.StoreStockStockConfiguration;

public class StoreStockStockConfiguration : IEntityTypeConfiguration<StoreStockLevel>
{
      public void Configure(EntityTypeBuilder<StoreStockLevel> builder)
      {
            builder.HasKey(s => s.Id);
            builder.HasOne(s => s.Store)
                  .WithMany()
                  .HasForeignKey(s => s.StoreId)
                  .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.Item)
                  .WithMany()
                  .HasForeignKey(s => s.ItemId)
                  .OnDelete(DeleteBehavior.Cascade);
      }
}