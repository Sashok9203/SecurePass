using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace data_access.Entities.Configs
{
    internal class CreditCardConfig : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasKey(x => x.CategoryId);
            builder.Property(x => x.ImageId);
            builder.Property(x => x.OwnerName).HasMaxLength(200);
            builder.Property(x => x.Type).HasMaxLength(50);
            builder.HasIndex(x => x.Number).IsUnique();
            builder.HasIndex(x => x.VerificationCode).IsUnique();
            builder.Property(x => x.Validity).HasColumnType("date");
            builder.Property(x => x.StartDate).HasColumnType("date");
            builder.ToTable(t => t.HasCheckConstraint("OwnerName_check", "[OwnerName] <> ''"));
            builder.ToTable(t => t.HasCheckConstraint("Type_check", "[Type] <> ''"));
            builder.HasOne(x => x.User).WithMany(x => x.CreditCards);
            builder.HasOne(x => x.Category).WithMany(x => x.CreditCards);
        }
    }
}
