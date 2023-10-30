using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace data_access.Entities.Configs
{
    internal class WifiConfig : IEntityTypeConfiguration<Wifi>
    {
        public void Configure(EntityTypeBuilder<Wifi> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasMaxLength(128);
            builder.Property(x => x.Name).HasMaxLength(64);
            builder.Property(x => x.Suranme).HasMaxLength(64);
            builder.Property(x => x.Gender);
            builder.Property(x => x.Birthday);
            builder.Property(t => t.WorkPlace).HasMaxLength(56);
            builder.Property(x => x.Company).HasMaxLength(64);
            builder.Property(x => x.Position).HasMaxLength(64);
            builder.ToTable(t => t.HasCheckConstraint("Title_check", "[Title] <> ''"));
        }
    }
}
