using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace data_access.Entities.Configs
{
    internal class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(56);
            builder.HasIndex(x => x.Name).IsUnique();
            builder.ToTable(t => t.HasCheckConstraint("Name_check", "[Name] <> ''"));
        }
    }
}
