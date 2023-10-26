namespace data_access.Entities
{
    public class SecureObject:BaseEntity
    {
        public bool IsFavorit { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
