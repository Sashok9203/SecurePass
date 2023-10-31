namespace data_access.Entities
{
    public class Universal: SecureObject
    {
        public string Label { get; set; }
        public string Value { get; set; }
        public int TypeId { get; set; }
    }
}
