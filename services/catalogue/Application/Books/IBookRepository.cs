
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Books;

public interface IBookRepository : IRepository<Book>
{
    Task<List<Book>> GetBooksAsync<BookDto>(CancellationToken cancellationToken);
}

public interface IRepository<TEntity>
{
    IEnumerable<TEntity> GetAll();

    TEntity Get(int id);

    TEntity Save(TEntity entity);

    void Delete(int entityId);
}