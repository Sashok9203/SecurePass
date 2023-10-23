namespace data_access.Entities
{
	public class WiFi : SecureObject
    {
		public int Id { get; set; }
		public string BaseStation { get; set; }
		public string Password { get; set; }
		public string IP { get; set; }
		public string AirPortId { get; set; }
		public string NetworkName { get; set; }
		public string WirelessSecurity { get; set; }
		public string WirelessPassword { get; set; }
        public string ConnectedStoragePasswords 
	}
}

