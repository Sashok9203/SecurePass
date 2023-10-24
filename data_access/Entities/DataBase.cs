using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access.Entities
{
    internal class DataBase:SecureObject
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public string Server { get; set; }
        public int Port { get; set; }
        public string Database {  get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string SID { get; set; }
        public string Pseudonym {  get; set; }
        public string ConnectionSettings { get; set; }
    }
}
