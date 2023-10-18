﻿using data_access.Entities;
using data_access.Entities.Configs;
using Microsoft.EntityFrameworkCore;

namespace data_access.Data
{
    public class SecurePassDBContext : DbContext
    {
        public SecurePassDBContext()
        {
             Database.EnsureDeleted();
             Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connect = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SecurePassDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Pooling = true";
            optionsBuilder.UseSqlServer(connect);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration<MyEntity>(new MyEntityConfig());
            modelBuilder.ApplyConfiguration<User>(new UserConfig());
            modelBuilder.ApplyConfiguration<Category>(new CategoryConfig());
            modelBuilder.ApplyConfiguration<Universal>(new UniversalConfig());

            ///...

            DefaultData.Initialize(modelBuilder);
        }
        //public DbSet<MyEntity> MyEntity { get; set; }
          public DbSet<User> Users { get; set; }
          public DbSet<Category> Categories { get; set; }
          public DbSet<Universal> Universals { get; set; }
    }
}
