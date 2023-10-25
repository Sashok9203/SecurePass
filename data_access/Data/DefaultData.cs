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
             new(){ Id = 5, ImageId = 3, Name = "Logins" },
             new(){ Id = 6, ImageId = 4, Name = "Cards" },
             new(){ Id = 7, ImageId = 3, Name = "Games" },
             new(){ Id = 8, ImageId = 4, Name = "Private"},
             new(){ Id = 9, ImageId = 3, Name = "Operating Systems" },
             new(){ Id = 10, ImageId = 4, Name = "DataBases" },
             new(){ Id = 11, ImageId = 3, Name = "Temporary" },
             new(){ Id = 12, ImageId = 4, Name = "BankAccounts"},
        };

        public static readonly Universal[] Universals =
        {
            new(){ Id = 1,ImageId = 2,  CategoryId = 1, Label = "Door pass", Title = "Password132", UserId = 1, Value ="24523" },
            new(){ Id = 2,ImageId = 4,  CategoryId = 1, Label = "KeyKey", Title = "PasswordY", UserId = 1, Value ="23key32" },
            new(){ Id = 3,ImageId = 3,  CategoryId = 3, Label = "luke22", Title = "Wordpress", UserId = 1, Value ="Hd783.Sv89x" },
            new(){ Id = 4,ImageId = 3,  CategoryId = 7, Label = "vera228", Title = "Steam", UserId = 2, Value ="gg7schu." },
            new(){ Id = 5,ImageId = 3,  CategoryId = 11, Label = "alexTmp@gmail.com", Title = "TempMail", UserId = 3, Value ="11223344" },

        };

        public static readonly CreditCard[] CreditCards =
        {
            new() { Id = 1, ImageId = 1, CategoryId = 6,Title = "card \"PrivateBank\"", OwnerName = "Veronika Malets", Type = "Type1", StartDate = new DateTime(2010, 10, 10) , Number ="00000000", UserId =2, Validity = new DateTime(2030, 10, 10), VerificationCode ="0030" },
            new() { Id = 2, ImageId = 2, CategoryId = 6,Title = "card \"OshadBank\"", OwnerName = "Veronika Malets", Type = "Type1", StartDate = new DateTime(2017, 8, 12) , Number ="00000000", UserId =2, Validity = new DateTime(2037, 8, 12), VerificationCode ="1230" },
            new() { Id = 3, ImageId = 4, CategoryId = 8,Title = "card \"Monobank\"", OwnerName = "Veronika Malets", Type = "Type1", StartDate = new DateTime(2020, 8, 11) , Number ="00000000", UserId =2, Validity = new DateTime(2040, 8, 11), VerificationCode ="1990" },
            new() { Id = 4, ImageId = 1, CategoryId = 9,Title = "card for pade \"Microsoft Store\"", OwnerName = "Alex Vini", Type = "Type1", StartDate = new DateTime(2020, 8, 11) , Number ="00000000", UserId =3, Validity = new DateTime(2040, 8, 11), VerificationCode ="1922" },
        };

        public static readonly Email[] emails =
        {
            new(){Id = 1, ImageId = 1, AuthenticationMethod ="Method", CategoryId = 2, Name ="someone@gmail.com", Password = "qwer1234", Server = "Server1", Port = 144, Type = "gmail", UserId = 1, Title = "Email1", Safety = "SafetyExample" },
            new(){Id = 2, ImageId = 2, AuthenticationMethod ="Method", CategoryId = 2, Name ="fagot88@ukrnet.com", Password = "qwerty88", Server = "Server2", Port = 123, Type = "ukr.net", UserId = 1, Title = "UkrNet", Safety = "SafetyExample" },
            new(){Id = 3, ImageId = 2, AuthenticationMethod ="Method", CategoryId = 5, Name ="vera234@gmail.com", Password = "vera0N1ka", Server = "Server3", Port = 11144, Type = "gmail", UserId = 2, Title = "Email", Safety = "SafetyExample" },
        };

        public static readonly Server[] Servers =
        {
            new() { Id = 1, ImageId= 3, CategoryId = 4, Title = "My server", Name = "Server1", URL = "url.www", Password = "urlpass", UserId = 1 },
            new() { Id = 2, ImageId= 4, CategoryId = 7, Title = "FactorioServer", Name = "factorio_team", URL = "hamachi.www", Password = "f123f123", UserId = 2 },
            new() { Id = 3, ImageId= 1, CategoryId = 9, Title = "UbuntuVirutalServer", Name = "ubuVirtual888", URL = "ubuntu.com", Password = "Hfkzm5K", UserId = 3 },
            new() { Id = 4, ImageId= 3, CategoryId = 11, Title = "TempServer", Name = "temp_server_3", URL = "hubspot.com", Password = "111111", UserId = 3 },
        };

        public static readonly DataBase[] DataBases=
        {
            new() { Id = 1, ImageId= 1, CategoryId = 3, Title = "My database", Type = "type1", Server = "my server", Port = 1234, Database = "My database", Name = "MyDatabase", Password = "database1", SID = "90900890", Pseudonym = "basebase", ConnectionSettings = "settings", UserId = 1 },
            new() { Id = 2, ImageId= 2, CategoryId = 4, Title = "My database1", Type = "type2", Server = "HubSpot", Port = 1889, Database = "HubSpot", Name = "HubSpot", Password = "hubSpotPass1", SID = "9090064890", Pseudonym = "hubspot", ConnectionSettings = "admin1", UserId = 1 },
            new() { Id = 3, ImageId= 1, CategoryId = 8, Title = "Database67", Type = "type3", Server = "server", Port = 1339, Database = "database", Name = "database_67", Password = "data67base", SID = "96733064890", Pseudonym = "database", ConnectionSettings = "admin89", UserId = 2 },
            new() { Id = 4, ImageId= 1, CategoryId = 10, Title = "Database1", Type = "type1", Server = "my server", Port = 1333, Database = "database", Name = "Database", Password = "database1", SID = "90900890", Pseudonym = "basebase", ConnectionSettings = "admin14", UserId = 3 },
            new() { Id = 5, ImageId= 2, CategoryId = 10, Title = "Database2", Type = "type8", Server = "HubSpot", Port = 1890, Database = "HubSpot", Name = "HubSpotDatabase", Password = "hubpass", SID = "90956756890", Pseudonym = "hubspot", ConnectionSettings = "admin13", UserId = 3 },
        };

        public static readonly BankAccount[] BankAccounts =
        {
            new() { Id = 1, Title = "My bank account", UserId = 2, CategoryId = 5, Name = "Monobank", OwnerName = "Veronika", Type = "type1", DepartmentNumber = 3, BankAccountNumber = "1234567", SWIFT = "ASDGSDVHHD", IBAN = "UA475734379878953987UA", PIN = "3342" },
            new() { Id = 2, Title = "My bank private bank account", UserId = 3, CategoryId = 12, Name = "Monobank", OwnerName = "Alex Vivo", Type = "type1", DepartmentNumber = 1, BankAccountNumber = "1234599067", SWIFT = "ASDGSDPPOPOVHHD", IBAN = "UA4757345345878953987UA", PIN = "2232" },
            new() { Id = 3, Title = "My FOP account", UserId = 3, CategoryId = 12, Name = "Monobank", OwnerName = "Alex Vivo", Type = "type4", DepartmentNumber = 2, BankAccountNumber = "123452344367", SWIFT = "ASDGHKJJHSDVHHD", IBAN = "UA4757334534578953987UA", PIN = "1142" },
        };
    }
}
