namespace data_access.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Universal> Universals { get; set; } = new HashSet<Universal>();
        public ICollection<CreditCard> CreditCards { get; set;} = new HashSet<CreditCard> ();
    }
}
