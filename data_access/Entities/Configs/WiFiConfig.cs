using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace data_access.Entities.Configs
{
    internal class WiFiConfig : IEntityTypeConfiguration<WiFi>
    {
        public void Configure(EntityTypeBuilder<WiFi> builder)
        {     
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.CetegoryId);
            builder.Property(x => x.BaseStation);
            builder.Property(x => x.Password);
            builder.Property(x => x.IP).HasMaxLength(64);
            builder.Property(x => x.AirPortId);
            builder.Property(x => x.NetworkName).HasMaxLength(128);
            builder.Property(x => x.WirelessSecurity).HasMaxLength(64);
            builder.Property(x => x.WirelessPassword).HasMaxLength(56);
        }
    }
}
