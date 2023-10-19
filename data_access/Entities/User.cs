namespace data_access.Entities
{
    public class User:BaseEntity
    {
        public string NikName { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<Universal> Universals { get; set; } = new HashSet<Universal>();
        public ICollection<CreditCard> CreditCards { get; set;} = new HashSet<CreditCard>();
        public ICollection<Email> Emails { get; set; } = new HashSet<Email>();
    }
}
