using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
            builder.Property(x => x.Title).HasMaxLength(128);
            builder.HasOne(x => x.Category).WithMany(x => x.Universals);
            builder.ToTable(t => t.HasCheckConstraint("Label_check", "[Label] <> ''"));
            builder.ToTable(t => t.HasCheckConstraint("Value_check", "[Value] <> ''"));
            builder.ToTable(t => t.HasCheckConstraint("Title_check", "[Title] <> ''"));
        }
    }
}
