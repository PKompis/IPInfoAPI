using IPInfo.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IPInfo.Data.Configurations
{
    public class IPConfiguration : IEntityTypeConfiguration<IP>
    {
        public void Configure(EntityTypeBuilder<IP> builder)
        {
            builder
                .HasKey(i => i.Ip);

            builder
                .Property(i => i.Ip)
                .HasMaxLength(100);

            builder
                .Property(i => i.City)
                .HasMaxLength(100);

            builder
                .Property(i => i.Continent)
                .HasMaxLength(100);

            builder
                .Property(i => i.Country)
                .HasMaxLength(100);

            builder
                .Property(i => i.Latitude)
                .HasColumnType("decimal(12,9)");

            builder
                .Property(i => i.Longitude)
                .HasColumnType("decimal(12,9)");

            builder
                .ToTable("IP");
        }
    }
}
