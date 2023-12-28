using Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Infrastructure.Data.Repositories;

public class BookRepository : MongoRepository<Book, Guid>, IBookRepository
{
    private readonly IMongoDbContext _dbContext;
    public BookRepository(IMongoDbContext context, IOptions<MongoDbOptions> options) : base(context, options)
    {
        _dbContext = context;
    }

    public async Task<List<Book>> GetBooksAsync<BookDto>(CancellationToken cancellationToken)
    {
        var queryable = _dbContext.GetCollection<Book>().AsQueryable();

        queryable = queryable.OrderBy(b => b.CreatedAt);

        return queryable.ToList();
    }
    //{
    //public async Task<BookDto> GetBooksAsync<BookDto>(BookParametersDto parameters, CancellationToken cancellationToken)
    //{
    //    var queryable = _dbContext.GetCollection<Book>().AsQueryable();
    //    if (!string.IsNullOrEmpty(parameters.Keyword))
    //    {
    //        string keyword = parameters.Keyword.ToLower();
    //        queryable = queryable.Where(t => t.Name.ToLower().Contains(keyword)
    //                                         || t.Details.ToLower().Contains(keyword)
    //                                         || t.Code.ToLower().Contains(keyword));
    //    }
    //    queryable = queryable.OrderBy(p => p.CreatedOn);
    //   // return await queryable.ApplyPagingAsync<Product, ProductDto>(parameters.PageNumber, parameters.PageSize, cancellationToken);
    //}
    public void Dispose()
    {
        throw new NotImplementedException();
    }
}