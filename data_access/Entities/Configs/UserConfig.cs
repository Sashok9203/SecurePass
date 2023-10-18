using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace data_access.Entities.Configs
{
    internal class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.NikName).HasMaxLength(56);
            builder.Property(x => x.PasswordHash).HasMaxLength(64);
            builder.HasIndex(x => x.NikName).IsUnique();
            builder.HasIndex(x => x.PasswordHash).IsUnique();
            builder.ToTable(t => t.HasCheckConstraint("NikName_check", "[NikName] <> ''"));
        }
    }
}
