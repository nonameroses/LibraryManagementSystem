using MongoDB.Driver;

namespace Infrastructure.Data;

public interface IMongoDbContext : IDisposable
{
    IMongoCollection<T> GetCollection<T>(string? name = null);
}