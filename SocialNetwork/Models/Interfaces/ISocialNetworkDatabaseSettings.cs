namespace SocialNetwork.Models.Interfaces
{
    public interface ISocialNetworkDatabaseSettings
    {
        public string PublicationsCollectionName { get; set; }

        public string CommentsCollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}
