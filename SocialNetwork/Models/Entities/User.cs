using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SocialNetwork.Models.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Nickname { get; set; }
        public string Residence { get; set; }
        [BsonElement("Birth")]
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Interests { get; set; }
    }
}
