using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access.Entities
{
    public class Universal: SecureObject
    {
        public string Label { get; set; }
        public string Value { get; set; }
    }
}
