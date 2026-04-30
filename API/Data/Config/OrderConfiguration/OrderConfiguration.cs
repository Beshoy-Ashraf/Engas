using API.Data.Models.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Data.Config.OrderConfiguration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
      public void Configure(EntityTypeBuilder<Order> builder)
      {
            builder.HasKey(s => s.Id);

            builder.HasOne(o => o.Customer)
                  .WithMany(c => c.Orders)
                  .HasForeignKey(o => o.CustomerId)
                  .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.Staff)
                  .WithMany(s => s.Orders)
                  .HasForeignKey(o => o.StaffId)
                  .OnDelete(DeleteBehavior.Cascade);

      }
}
