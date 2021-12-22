using MongoDB.Driver;

namespace AuthenticationService.Infrastructure.DatabaseContext
{
    public sealed class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IMongoClient mongoClient, string databaseName)
            => _database = mongoClient.GetDatabase(databaseName);

        public IMongoCollection<T> GetCollection<T>(string collectionName)
            => _database.GetCollection<T>(collectionName); 
    }
}
