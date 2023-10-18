namespace data_access.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Universal> Universals { get; set; } = new List<Universal>();
    }
}
