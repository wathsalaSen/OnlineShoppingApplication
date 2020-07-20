using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShopping.Common.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int MobileNumber { get; set; }
        public string Email { get; set; }
    }
}
