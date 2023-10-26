using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace data_access.Entities.Configs
{
    internal class ContactConfig : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasMaxLength(128);
            builder.Property(x => x.Name).HasMaxLength(64);
            builder.Property(x => x.Surname).HasMaxLength(64);
            builder.HasOne(x => x.User).WithMany(x => x.Contacts);
            builder.HasOne(x => x.Category).WithMany(x => x.Contacts);
            builder.Property(x => x.Birthday);
            builder.Property(x => x.WorkPLace).HasMaxLength(64);
            builder.Property(x => x.Company).HasMaxLength(64);
            builder.Property(x => x.Position).HasMaxLength(64);
            builder.ToTable(t => t.HasCheckConstraint("Title_check", "[Title] <> ''"));
        }
    }
}
