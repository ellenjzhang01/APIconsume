using System;

namespace API.Common.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public int Age { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string State { get; set; }
    }
}
