using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string NikName { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<Universal> Universals { get; set; } = new HashSet<Universal>();
        public ICollection<CreditCard> CreditCards { get; set;} = new HashSet<CreditCard>();
    }
}
