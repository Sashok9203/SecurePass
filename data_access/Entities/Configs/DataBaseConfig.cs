using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access.Entities.Configs
{
    internal class DataBaseConfig : IEntityTypeConfiguration<DataBase>
    {
        public void Configure(EntityTypeBuilder<DataBase> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasMaxLength(128);
            builder.ToTable(t => t.HasCheckConstraint("Title_check", "[Title] <> ''"));
            builder.HasOne(x => x.Category).WithMany(x => x.DataBases);
            builder.Property(x => x.Type).HasMaxLength(50);
            builder.Property(x => x.Server).HasMaxLength(128);
            builder.Property(x => x.Port);
            builder.Property(x => x.Database).HasMaxLength(128);
            builder.Property(x => x.Name).HasMaxLength(128);
            builder.Property(x => x.Password).HasMaxLength(128);
            builder.Property(x => x.SID).HasMaxLength(128);
            builder.Property(x => x.Pseudonym).HasMaxLength(128);
            builder.Property(x => x.ConnectionSettings).HasMaxLength(128);
        }
    }
}
