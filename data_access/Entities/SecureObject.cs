namespace data_access.Entities
{
    public class SecureObject:BaseEntity
    {
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
