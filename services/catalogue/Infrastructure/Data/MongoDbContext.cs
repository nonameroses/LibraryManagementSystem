using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Domain.Entities;

namespace Infrastructure.Data;

public class MongoDbContext : IMongoDbContext
{
    public IMongoDatabase Database { get; }
    public IMongoClient MongoClient { get; }

    public MongoDbContext(IOptions<MongoDbOptions> options)
    {
        MongoClient = new MongoClient(options.Value.ConnectionString);
        string databaseName = options.Value.DatabaseName;
        Database = MongoClient.GetDatabase(databaseName);
    }


    public IMongoCollection<T> GetCollection<T>(string? name = null)
    {
        return Database.GetCollection<T>(name ?? typeof(T).Name.ToLower());
    }

}