using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access.Entities.Configs
{
    internal class UniversalConfig : IEntityTypeConfiguration<Universal>
    {
        public void Configure(EntityTypeBuilder<Universal> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ImageId);
            builder.Property(x => x.Label).HasMaxLength(256);
            builder.Property(x => x.Value);
            builder.Property(x => x.TypeId);
            builder.HasOne(x => x.User).WithMany(x => x.Universals);
            builder.HasOne(x => x.Category).WithMany(x => x.Universals);
            builder.ToTable(t => t.HasCheckConstraint("Label_check", "[Label] <> ''"));
            builder.ToTable(t => t.HasCheckConstraint("Value_check", "[Value] <> ''"));
        }
    }
}
