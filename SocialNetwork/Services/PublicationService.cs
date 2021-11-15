using SocialNetwork.Models.Entities;
using SocialNetwork.Models.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
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
            _publications.Find(Publication => true).ToList();

        public async Task CreateAsync(ApplicationPublication Publication)
        {
            await _publications.InsertOneAsync(Publication);
        }

        public void Update(string id, ApplicationPublication PublicationIn) =>
            _publications.ReplaceOne(Publication => Publication.Id == id, PublicationIn);
    }
}
