using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access.Entities
{
    public class Server: SecureObject
    {
        public string URL { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
