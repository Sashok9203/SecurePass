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
    }
}
