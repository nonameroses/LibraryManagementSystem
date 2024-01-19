using Domain.Entities;
using MediatR;

namespace Application.Books.Features.Queries;

//Query to get all the books 
public class GetBooks
{
    public sealed class Query : IRequest<IEnumerable<Book>>
    {
        public List<Book> Books { get; set; }

        public Query()
        {

        }
    }

    // Takes in Query and returns a List of Books
    public class Handler : IRequestHandler<Query, IEnumerable<Book>>
    {
        // Declare Mongo Repo class to use the methods
        private readonly IMongoRepository<Book> _mongoRepository;

        // Injecting dependency into constructor
        public Handler(IMongoRepository<Book> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<IEnumerable<Book>> Handle(Query request, CancellationToken cancellationToken)
        {
            // Calling the repo to return all the books
            var books = _mongoRepository.AsQueryable();

            return books;
        }
    }
}
