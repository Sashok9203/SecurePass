using Microsoft.EntityFrameworkCore;

namespace data_access.Data
{
    public class SecurePassDBContext : DbContext
    {
        public SecurePassDBContext()
        {
            //Database.EnsureDeleted();
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


        }

        //public DbSet<MyEntity> MyEntity { get; set; }
       

    }
}
