using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access.Entities.Configs
{
    internal class EmailConfig : IEntityTypeConfiguration<Email>
    {
        public void Configure(EntityTypeBuilder<Email> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ImageId);
            builder.Property(x => x.Type).HasMaxLength(128);
            builder.Property(x => x.Name).HasMaxLength(128);
            builder.Property(x => x.Server).HasMaxLength(128);
            builder.HasIndex(x => x.Port);
            builder.Property(x => x.Port).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(128);
            builder.Property(x => x.Safety).HasMaxLength(128);
            builder.Property(x => x.AuthenticationMethod).HasMaxLength(128);
            builder.HasOne(x => x.User).WithMany(x => x.Emails);
            builder.HasOne(x => x.Category).WithMany(x => x.Emails);
        }
    }
}
