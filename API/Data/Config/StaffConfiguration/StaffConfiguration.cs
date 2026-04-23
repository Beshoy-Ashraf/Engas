using API.Data.Models.Staff;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Data.Config.StaffConfiguration;

public class StaffConfiguration : IEntityTypeConfiguration<Staff>
{
      public void Configure(EntityTypeBuilder<Staff> builder)
      {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.UserName).IsRequired();
            builder.HasIndex(s => s.UserName).IsUnique();
            builder.Property(s => s.Password).IsRequired();

      }
}
