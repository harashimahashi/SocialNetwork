using SocialNetwork.Models.Entities;
using SocialNetwork.Models.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Services
{
    public class CommentService
    {
        private readonly IMongoCollection<ApplicationComment> _comments;

        public CommentService(ISocialNetworkDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _comments = database.GetCollection<ApplicationComment>(settings.CommentsCollectionName);
        }

        public List<ApplicationComment> Get() =>
            _comments.Find(_ => true).ToList();

        public async Task<ApplicationComment> GetByIdAsync(string id) => (await _comments.FindAsync(com => com.Id == id)).First();

        public async Task<List<ApplicationComment>> GetByPublicationIdAsync(string id)
            => await (await _comments.FindAsync(com => com.Publication == id)).ToListAsync();

        public async Task<ApplicationComment> UpdateOneAsync<T>(string id, string fieldName, T value)
            => await _comments.FindOneAndUpdateAsync(el => el.Id == id, Builders<ApplicationComment>.Update.AddToSet(fieldName, value));

        public async Task UpdateOneAsync(string id, ApplicationComment com)
            => await _comments.ReplaceOneAsync(el => el.Id == id, com);

        public async Task CreateAsync(ApplicationComment comment)
        {
            await _comments.InsertOneAsync(comment);
        }
    }
}
