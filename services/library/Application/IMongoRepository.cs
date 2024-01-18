using System.Linq.Expressions;
using Domain.Entities;

namespace Application;

public interface IMongoRepository<TDocument> where TDocument : IDocument
{
    IQueryable<TDocument> AsQueryable();
    TDocument FindOne(Expression<Func<TDocument, bool>> filterExpression);

    Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression);
    IQueryable<TDocument> FindMultipleByIdAsync(IEnumerable<string> id);

    Task InsertOneAsync(TDocument document);

    Task ReplaceOneAsync(TDocument document);

    Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression);

}