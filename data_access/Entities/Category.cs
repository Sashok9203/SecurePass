namespace data_access.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Universal> Universals { get; set; } = new HashSet<Universal>();
        public ICollection<CreditCard> CreditCards { get; set;} = new HashSet<CreditCard> ();
        public ICollection<Email> Emails { get; set; } = new HashSet<Email> ();
        public ICollection<Server> Servers { get; set;} = new HashSet<Server> ();
        public ICollection<DataBase> DataBases { get; set; } = new HashSet<DataBase>();
        public ICollection<BankAccount> BankAccounts { get; set; } = new HashSet<BankAccount>();
        public ICollection<WiFi> WiFis { get; set; } = new HashSet<WiFi>();
        public ICollection<Contact> Contacts { get; set; } = new HashSet<Contact>();
    }
}
