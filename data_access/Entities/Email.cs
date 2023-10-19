namespace data_access.Entities
{
    public class Email : SecureObject
    {
        public string Type { get; set; }
        public string Name{ get; set; }
        public string Server { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
        public string Safety { get; set; }
        public string AuthenticationMethod { get; set; }
    }
}
