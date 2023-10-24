using data_access.Entities;
using Microsoft.EntityFrameworkCore;

namespace data_access.Data
{
    internal static class DefaultData
    {

        public static void Initialize(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(Users);
            modelBuilder.Entity<Category>().HasData(Categories);
            modelBuilder.Entity<Universal>().HasData(Universals);
            modelBuilder.Entity<CreditCard>().HasData(CreditCards);
            modelBuilder.Entity<Server>().HasData(Servers);
            modelBuilder.Entity<BankAccount>().HasData(BankAccounts);
        }

        public static readonly User[] Users =
        {
            new User(){ Id = 1, ImageId = 1, NikName = "Fagot" , PasswordHash = "19513fdc9da4fb72a4a05eb66917548d3c90ff94d5419e1f2363eea89dfee1dd"},     //Password1
            new User(){ Id = 2, ImageId = 2, NikName = "Veronica" , PasswordHash = "1be0222750aaf3889ab95b5d593ba12e4ff1046474702d6b4779f4b527305b23"},  //Password2
            new User(){ Id = 3, ImageId = 3, NikName = "Alex" , PasswordHash = "2538f153f36161c45c3c90afaa3f9ccc5b0fa5554c7c582efe67193abb2d5202"}       //Password3
        };

        public static readonly Category[] Categories =
        {
             new(){ Id = 1, ImageId = 3, Name = "Passwords" },
             new(){ Id = 2, ImageId = 4, Name = "Emails" },
             new(){ Id = 3, ImageId = 3, Name = "Sites" },
             new(){ Id = 4, ImageId = 4, Name = "Servers"},
        };

        public static readonly Universal[] Universals =
        {
            new(){ Id = 1,ImageId = 2,  CategoryId = 1, Label = "Door pass", Title = "Password", UserId = 1, Value  ="24523" }
        };

        public static readonly CreditCard[] CreditCards =
        {
            new() { Id = 1, ImageId = 1, CategoryId = 2,Title = "My credit card", OwnerName = "John Doe", Type = "Type1", StartDate = new DateTime(2010, 10, 10) , Number ="00000000", UserId =1, Validity = new DateTime(2030, 10, 10), VerificationCode ="0000" }
        };

        public static readonly Email[] emails =
        {
            new(){Id = 1, ImageId = 1, AuthenticationMethod ="Method", CategoryId = 2, Name ="someone@gmail.com", Password = "qwer1234", Server = "Server1", Port = 1, Type = "gmail", UserId = 1, Title = "Email1", Safety = "SafetyExample" }
        };

        public static readonly Server[] Servers =
        {
            new() { Id = 1, ImageId= 3, CategoryId = 4, Title = "My server", Name = "Server1", URL = "url.www", Password = "urlpass", UserId = 1 }
        };

        public static readonly DataBase[] DataBases=
        {
            new() { Id = 1, ImageId= 1, CategoryId = 1, Title = "My database", Type = "type", Server = "my server", Port = 1234, Database = "My database", Name = "Database1", Password = "database1", SID = "90900890", Pseudonym = "basebase", ConnectionSettings = "settings", UserId = 1 }
        };

        public static readonly BankAccount[] BankAccounts =
        {
            new() { Id = 1, Title = "My bank account", UserId = 1, CategoryId = 1, Name = "Name account", OwnerName = "Name owner", Type = "My type", DepartmentNumber = 1, BankAccountNumber = "1234567", SWIFT = "XXXXXXX", IBAN = "XX000000000XX", PIN = "0000" }
        };
    }
}
