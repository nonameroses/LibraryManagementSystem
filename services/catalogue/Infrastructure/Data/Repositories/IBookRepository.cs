
using Domain.Entities;

namespace Infrastructure.Data.Repositories
{
    public interface IBookRepository
    {
        Task<List<Book>> GetBooksAsync<BookDto>(CancellationToken cancellationToken);
    }
}