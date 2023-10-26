using System;

namespace data_access.Entities
{
    public class Contact : SecureObject
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string WorkPLace { get; set; }
        public string Company { get; set; }
        public string Position { get; set; }
    }
}