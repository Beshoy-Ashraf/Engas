using API.Data.Models.Store;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Data.Config.StoreConfiguration;

public class StoreConfiguration : IEntityTypeConfiguration<Store>
{
      public void Configure(EntityTypeBuilder<Store> builder)
      {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Code).IsRequired();
            builder.HasIndex(s => s.Code).IsUnique();
            builder.Property(s => s.Password).IsRequired();
      }
}
