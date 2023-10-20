using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace data_access.Entities.Configs
{
    internal class ContactConfig : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CategoryId);
            builder.Property(x=> x.Name).HasMaxLength(56);
            builder.Property(x => x.Surname).HasMaxLength(64);
            builder.Property(x => x.Gender);
            builder.Property(x => x.Birthday);
            builder.Property(x => x.Workplace).HasMaxLength(128);
            builder.Property(X => X.Position).HasMaxLength(128);
            builder.Property(x => x.UserId);
        }
    }
}
