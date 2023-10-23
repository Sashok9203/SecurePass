using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace data_access.Entities.Configs
{
    internal class WifiConfig : IEntityTypeConfiguration<Wifi>
    {
        public void Configure(EntityTypeBuilder<Wifi> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CategoryId);
            builder.Property(x => x.BaseStation).HasMaxLength(64);
            builder.Property(x => x.Password).HasMaxLength(64);
            builder.Property(x => x.IP);
            builder.Property(x => x.AirPortId);
            builder.Property(t => t.NetworkName).HasMaxLength(56);
            builder.Property(x => x.WirelessSecurity).HasMaxLength(64);
            builder.Property(x => x.WirelessPassword).HasMaxLength(64);
            builder.Property(x => x.UserId).HasMaxLength(64);
        }
    }
}
