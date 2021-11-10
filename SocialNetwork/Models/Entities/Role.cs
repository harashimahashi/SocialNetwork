using System;
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace SocialNetwork.Models.Entities
{
    [CollectionName("Roles")]
    public class Role : MongoIdentityRole<Guid>
    {
    }
}
