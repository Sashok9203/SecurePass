using data_access.Entities;
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
            modelBuilder.ApplyConfiguration<BankAccount>(new BankAccountConfig());


        }

        public DbSet<BankAccount> BankAccount{ get; set; }
       

    }
}
