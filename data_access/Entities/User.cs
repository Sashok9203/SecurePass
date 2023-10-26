namespace data_access.Entities
{
    public class User:BaseEntity
    {
        public string NikName { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<Category> Categories { get; set; } = new HashSet<Category>();
        
    }
}
