using Domain.Entities;
using Domain.Entities.Abstractions;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Infrastructure.Data;

public class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : IDocument
{
    private readonly IMongoCollection<TDocument> _collection;
    private readonly IMongoDbContext _context;

   public MongoRepository(IMongoDbContext context, IOptions<MongoOptions> options)
    {
        //MongoClient client = new MongoClient(options.Value.ConnectionString);
        //IMongoDatabase database = client.GetDatabase(options.Value.DatabaseName);
        _context = context;
        _collection = _context.GetCollection<TDocument>(options.Value.CollectionName);

        //var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
        //MongoClient client = new MongoClient(settings.Value.ConnectionString);
    }

    public virtual IQueryable<TDocument> AsQueryable()
    {
        return _collection.AsQueryable();
    }

    public virtual IEnumerable<TDocument> FilterBy(
        Expression<Func<TDocument, bool>> filterExpression)
    {
        return _collection.Find(filterExpression).ToEnumerable();
    }

    public virtual IEnumerable<TProjected> FilterBy<TProjected>(
        Expression<Func<TDocument, bool>> filterExpression,
        Expression<Func<TDocument, TProjected>> projectionExpression)
    {
        return _collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
    }


    public virtual Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression)
    {
        return Task.Run(() => _collection.Find(filterExpression).FirstOrDefaultAsync());
    }

    public virtual Task<TDocument> FindByIdAsync(string id)
    {
        return Task.Run(() =>
        {
            var objectId = new ObjectId(id);
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
            return _collection.Find(filter).SingleOrDefaultAsync();
        });
    }


    public async Task<bool> ExistsAsync(Expression<Func<TDocument, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(predicate).AnyAsync(cancellationToken: cancellationToken)!;
    }

    public async Task<IReadOnlyList<TDocument>> FindAsync(Expression<Func<TDocument, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(predicate).ToListAsync(cancellationToken: cancellationToken)!;
    }

    public Task<TDocument?> FindOneAsync(Expression<Func<TDocument, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return _collection.Find(predicate).SingleOrDefaultAsync(cancellationToken: cancellationToken)!;
    }



    public async Task<IReadOnlyList<TDocument>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _collection.AsQueryable().ToListAsync(cancellationToken);
    }

    public async Task AddAsync(TDocument document, CancellationToken cancellationToken = default)
    {
        await _collection.InsertOneAsync(document, new InsertOneOptions(), cancellationToken);
    }

    public async Task UpdateAsync(TDocument entity, CancellationToken cancellationToken = default)
    {
        await _collection.ReplaceOneAsync(x => x.Id!.Equals(entity.Id), entity, cancellationToken: cancellationToken);
    }

    public Task DeleteRangeAsync(IReadOnlyList<TDocument> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Expression<Func<TDocument, bool>> predicate, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(TDocument entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }


}
