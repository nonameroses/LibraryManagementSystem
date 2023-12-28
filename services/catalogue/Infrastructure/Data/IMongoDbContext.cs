using MongoDB.Driver;

namespace Infrastructure.Data
{
    public interface IMongoDbContext
    {
        IMongoDatabase Database { get; }
        IMongoClient MongoClient { get; }

        IMongoCollection<T> GetCollection<T>(string? name = null);
    }
}