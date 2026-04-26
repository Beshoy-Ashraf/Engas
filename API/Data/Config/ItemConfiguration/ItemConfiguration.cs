using API.Data.Models.Item;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Data.Config.ItemConfiguration;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
      public void Configure(EntityTypeBuilder<Item> builder)
      {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Model).IsRequired();
            builder.Property(s => s.Brand).IsRequired();

      }
}
