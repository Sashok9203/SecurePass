using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access.Entities
{
    internal class WiFi:SecureObject
    {
        public int Id { get; set; }
        public int CetegoryId { get; set; }
        public string BaseStation { get; set; }
        public string Password { get; set; }
        public string IP {  get; set; }
        public string AirPortId { get; set; }
        public string NetworkName { get; set; }
        public string WirelessSecurity { get; set; }
        public string WirelessPassword { get; set; }
        public string ConnectedStoragePasswords { get; set; }

    }
}
