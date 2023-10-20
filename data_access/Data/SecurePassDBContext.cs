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
            modelBuilder.ApplyConfiguration<CreditCard>(new CreditCardConfig());
            modelBuilder.ApplyConfiguration<Email>(new EmailConfig());
            modelBuilder.ApplyConfiguration<User>(new UserConfig());
            modelBuilder.ApplyConfiguration<Category>(new CategoryConfig());
            modelBuilder.ApplyConfiguration<Universal>(new UniversalConfig());
            modelBuilder.ApplyConfiguration<Server>(new ServerConfig());
            modelBuilder.ApplyConfiguration<WiFi>(new WiFiConfig());
            modelBuilder.ApplyConfiguration<Contact>(new ContactConfig());

            DefaultData.Initialize(modelBuilder);
        }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Universal> Universals { get; set; }
        public DbSet<Server> Servers { get; set; }
    }
}
