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
            builder.Property(x => x.OwnerName).HasMaxLength(200);
            builder.Property(x => x.Type).HasMaxLength(50);
            builder.HasIndex(x => x.Number).IsUnique();
            builder.HasIndex(x => x.VerificationCode).IsUnique();
           


        }
    }
}
