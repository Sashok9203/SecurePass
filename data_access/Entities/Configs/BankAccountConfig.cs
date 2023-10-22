﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace data_access.Entities.Configs
{
    internal class BankAccountConfig : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(56);
            builder.HasIndex(x => x.Name).IsUnique();
            builder.ToTable(t => t.HasCheckConstraint("Name_check", "[Name] <> ''"));
            builder.Property(a => a.OwnerName).HasMaxLength(56);
            builder.HasIndex(a => a.OwnerName).IsUnique();
            builder.ToTable(q => q.HasCheckConstraint("OwnerName_check", "[OwnerName] <> ''"));
            builder.Property(w => w.Type).HasMaxLength(56);
            builder.Property(w => w.DepartmentNumber).HasMaxLength(56);
            builder.Property(x => x.BankAccountNumber).HasMaxLength(56);
            builder.Property(x => x.SWIFT).HasMaxLength(56);
            builder.Property(x => x.IBAN).HasMaxLength(56);
            builder.Property(x => x.PIN).HasMaxLength(56);
            builder.Property(x => x.Title).HasMaxLength(128);
            builder.ToTable(t => t.HasCheckConstraint("Title_check", "[Title] <> ''"));

            throw new NotImplementedException();
        }

    }
}
