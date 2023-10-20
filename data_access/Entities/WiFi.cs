namespace data_access.Entities
{
	public class WiFi
	{
		public int Id { get; set; }
		public int CategoryId { get; set; }
		public string BaseStation { get; set; }
		public string Password { get; set; }
		public string IP { get; set; }
		public string AirPortId { get; set; }
		public string NetworkName { get; set; }
		public string WirelessSecurity { get; set; }
		public string WirelessPassword { get; set; }
        public ICollection<WiFi> ConnectedStoragePasswords { get; set; } = new HashSet<WiFi>();
        public int UserId { get; set; }
	}
}

