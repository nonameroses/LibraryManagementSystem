using MongoDB.Driver;

namespace Infrastructure.Data;

public interface IMongoDbContext
{
    IMongoCollection<T> GetCollection<T>(string? name = null);
}