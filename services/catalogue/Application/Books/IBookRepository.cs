using Domain.Entities;
using Infrastructure.Data;

namespace Application.Books;

public interface IBookRepository : IMongoRepository<Book, Guid>
{
    void GetBooksAsync<BookDto>(CancellationToken cancellationToken);
}