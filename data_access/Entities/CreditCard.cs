using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace data_access.Entities
{
    public class CreditCard
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string OwnerName { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }
        public string VerificationCode { get; set; }

        public DateTime Validity { get; set; }
        public DateTime StartDate { get; set; }

        public int UserId { get; set; }


    }
}