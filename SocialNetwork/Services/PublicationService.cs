using SocialNetwork.Models.Entities;
using SocialNetwork.Models.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Services
{
    public class PublicationService
    {
        private readonly IMongoCollection<ApplicationPublication> _publications;

        public PublicationService(ISocialNetworkDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _publications = database.GetCollection<ApplicationPublication>(settings.PublicationsCollectionName);
        }

        public List<ApplicationPublication> Get() =>
            _publications.Find(_ => true).ToList();

        public async Task<ApplicationPublication> GetByIdAsync(string id) => (await _publications.FindAsync(pub => pub.Id == id)).First();

        public async Task<List<ApplicationPublication>> GetByOwnerIdsAsync(List<Guid> ids) 
            => await (await _publications.FindAsync(pub => ids.Contains(pub.Owner))).ToListAsync();

        public async Task<ApplicationPublication> UpdateOneAsync<T>(string id, string fieldName, T value) 
            => await _publications.FindOneAndUpdateAsync(el => el.Id == id, Builders<ApplicationPublication>.Update.AddToSet(fieldName, value));

        public async Task UpdateOneAsync(string id, ApplicationPublication pub)
            => await _publications.ReplaceOneAsync(el => el.Id == id, pub);

        public async Task CreateAsync(ApplicationPublication Publication)
        {
            await _publications.InsertOneAsync(Publication);
        }
    }
}
