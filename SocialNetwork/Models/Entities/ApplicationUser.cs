using System;
using System.Collections.Generic;
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace SocialNetwork.Models.Entities
{
    [CollectionName("Users")]
    public class ApplicationUser : MongoIdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Residence { get; set; }

        public DateTime BirthDate { get; set; }

        public string Interests { get; set; }

        public byte[] Image { get; set; }
        public List<Guid> Subscribed { get; set; }
        public int Subscribers { get; set; } = 0;
    }
}
