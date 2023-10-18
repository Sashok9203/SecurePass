namespace data_access.Entities
{
    public class CreditCard:SecureObject
    {
        public string OwnerName { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }
        public string VerificationCode { get; set; }
        public DateTime Validity { get; set; }
        public DateTime StartDate { get; set; }
    }
}