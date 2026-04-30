using API.Data.Models.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Data.Config.OrderItemConfiguration;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
      public void Configure(EntityTypeBuilder<OrderItem> builder)
      {
            builder.HasKey(s => s.Id);

            builder.HasOne(oi => oi.Order)
                  .WithMany(o => o.OrderItems)
                  .HasForeignKey(oi => oi.OrderId)
                  .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(oi => oi.Item)
                  .WithMany(i => i.OrderItems)
                  .HasForeignKey(oi => oi.ItemId)
                  .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(oi => oi.Store)
                  .WithMany(s => s.OrderItems)
                  .HasForeignKey(oi => oi.StoreId)
                  .OnDelete(DeleteBehavior.Cascade);
      }
}
