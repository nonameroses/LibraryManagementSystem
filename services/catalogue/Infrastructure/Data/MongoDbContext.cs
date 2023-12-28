using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Domain.Entities;

namespace Infrastructure.Data;

public class MongoDbContext : IMongoDbContext
{
    public IMongoDatabase Database { get; }
    public IMongoClient MongoClient { get; }

    public MongoDbContext(IOptions<MongoOptions> options)
    {
        MongoClient = new MongoClient(options.Value.ConnectionString);
        Database = MongoClient.GetDatabase(options.Value.DatabaseName);
    }


    public IMongoCollection<T> GetCollection<T>(string? name = null)
    {
        return Database.GetCollection<T>(name ?? typeof(T).Name.ToLower());
    }
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}