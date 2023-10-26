using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace data_access.Entities.Configs
{
    internal class WiFiConfig : IEntityTypeConfiguration<WiFi>
    {
        public void Configure(EntityTypeBuilder<WiFi> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasMaxLength(128);
            builder.Property(x => x.BaseStation).HasMaxLength(64);
            builder.Property(x => x.Password).HasMaxLength(64);
            builder.Property(x => x.IP);
            builder.Property(x => x.AirPortId);
            builder.Property(t => t.NetworkName).HasMaxLength(56);
            builder.Property(x => x.WirelessSecurity).HasMaxLength(64);
            builder.Property(x => x.WirelessPassword).HasMaxLength(64);
            builder.ToTable(t => t.HasCheckConstraint("Title_check", "[Title] <> ''"));
        }
    }
}
