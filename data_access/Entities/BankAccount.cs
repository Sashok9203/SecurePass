using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access.Entities
{
    public class BankAccount:SecureObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OwnerName { get; set; }
        public string Type { get; set; }
        public int DepartmentNumber { get; set; }
        public string BankAccountNumber { get; set; }
        public string SWIFT { get; set; }
        public string IBAN { get; set; }
        public string PIN { get; set; }
    }
}
