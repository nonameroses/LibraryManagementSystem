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

    public class Handler : IRequestHandler<Query, IEnumerable<Book>>
    {
        private readonly IMongoRepository<Book> _mongoRepository;

        public Handler(IMongoRepository<Book> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<IEnumerable<Book>> Handle(Query request, CancellationToken cancellationToken)
        {
            var books = _mongoRepository.AsQueryable();

            return books;
        }
    }
}
