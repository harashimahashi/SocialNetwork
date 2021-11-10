using System;

namespace SocialNetwork.Models.Entities
{
    public class User
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Residence { get; set; }

        public DateTime BirthDate { get; set; }

        public string Interests { get; set; }

        public string Password { get; set; }
    }
}
