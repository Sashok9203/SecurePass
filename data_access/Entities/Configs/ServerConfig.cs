using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace data_access.Entities.Configs
{
    internal class ServerConfig : IEntityTypeConfiguration<Server>
    {
        public void Configure(EntityTypeBuilder<Server> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ImageId);
            builder.HasOne(x => x.User).WithMany(x => x.Servers);
            builder.HasOne(x => x.Category).WithMany(x => x.Servers);
            builder.Property(x => x.URL).HasMaxLength(128);
            builder.Property(x => x.Name).HasMaxLength(128);
            builder.Property(x => x.Password).HasMaxLength(128);
            builder.ToTable(t => t.HasCheckConstraint("URL_check", "[URL] <> ''"));
            builder.ToTable(t => t.HasCheckConstraint("Name_check", "[Name] <> ''"));
            builder.ToTable(t => t.HasCheckConstraint("Password_check", "[Password] <> ''"));
        }
    }
}
