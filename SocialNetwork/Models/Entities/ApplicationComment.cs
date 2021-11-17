using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace SocialNetwork.Models.Entities
{
    public class ApplicationComment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public Guid Owner { get; set; }

        public string Publication { get; set; }

        public DateTime Created { get; set; }

        public string Content { get; set; }

        public List<Guid> Liked { get; set; } = new();
    }
}
