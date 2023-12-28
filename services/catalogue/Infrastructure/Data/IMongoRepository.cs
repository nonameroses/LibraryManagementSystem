using Domain.Entities.Abstractions;
using System.Linq.Expressions;

namespace Infrastructure.Data
{
    public interface IMongoRepository<TDocument> where TDocument : IDocument
    {
        IQueryable<TDocument> AsQueryable();
        Task DeleteByIdAsync(string id);
        void DeleteMany(Expression<Func<TDocument, bool>> filterExpression);
        Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression);
        Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression);
        IEnumerable<TDocument> FilterBy(Expression<Func<TDocument, bool>> filterExpression);
        IEnumerable<TProjected> FilterBy<TProjected>(Expression<Func<TDocument, bool>> filterExpression, Expression<Func<TDocument, TProjected>> projectionExpression);
        Task<TDocument> FindByIdAsync(string id);
        Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression);
        Task InsertManyAsync(ICollection<TDocument> documents);
        Task InsertOneAsync(TDocument document);
        Task ReplaceOneAsync(TDocument document);
    }
}