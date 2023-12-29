using Domain.Entities.Abstractions;
using System.Linq.Expressions;

namespace Infrastructure.Data
{
    public interface IMongoRepository<TDocument> where TDocument : IDocument
    {

        Task<TDocument?> FindOneAsync(Expression<Func<TDocument, bool>> predicate, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<TDocument>> FindAsync(Expression<Func<TDocument, bool>> predicate, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<TDocument>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Expression<Func<TDocument, bool>> predicate, CancellationToken cancellationToken = default);

        Task AddAsync(TDocument entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(TDocument entity, CancellationToken cancellationToken = default);
        Task DeleteRangeAsync(IReadOnlyList<TDocument> entities, CancellationToken cancellationToken = default);
        Task DeleteAsync(Expression<Func<TDocument, bool>> predicate, CancellationToken cancellationToken = default);
        Task DeleteAsync(TDocument entity, CancellationToken cancellationToken = default);
    }
}