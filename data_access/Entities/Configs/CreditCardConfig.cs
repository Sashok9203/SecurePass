using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace data_access.Entities.Configs
{
    internal class CreditCardConfig : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ImageId);
            builder.Property(x => x.Title).HasMaxLength(128);
            builder.Property(x => x.OwnerName).HasMaxLength(200);
            builder.Property(x => x.Type).HasMaxLength(50);
            builder.Property(x => x.Validity).HasColumnType("date");
            builder.Property(x => x.StartDate).HasColumnType("date");
            builder.ToTable(t => t.HasCheckConstraint("OwnerName_check", "[OwnerName] <> ''"));
            builder.ToTable(t => t.HasCheckConstraint("Type_check", "[Type] <> ''"));
            builder.ToTable(t => t.HasCheckConstraint("Title_check", "[Title] <> ''"));
            builder.HasOne(x => x.User).WithMany(x => x.CreditCards);
            builder.HasOne(x => x.Category).WithMany(x => x.CreditCards);
        }
    }
}
