using Application.Consumer;
using Domain.Entities;
using MediatR;

namespace Application.Books.Features.Queries;

//Query to get a books by Id
public class GetBooksById
{
    public sealed class Query : IRequest<IEnumerable<Book>>
    {
        public List<string> Ids { get; set; }

        public Query(List<string> ids)
        {
            Ids = ids;
        }
    }
    public class Handler : IRequestHandler<Query, IEnumerable<Book>>
    {
        private readonly IMongoRepository<Book> _mongoRepository;
        private readonly IRabbitMqConsumerService _consumer;

        public Handler(IMongoRepository<Book> mongoRepository, IRabbitMqConsumerService consumer)
        {
            _mongoRepository = mongoRepository;
            _consumer = consumer;
        }

        public async Task<IEnumerable<Book>> Handle(Query request, CancellationToken cancellationToken)
        {
            // Consumes the message produced from other microservice, however, the implementation
            // is not fully finished.
            var items = _consumer.ConsumeMessage();
            // returns a list of books matching the Id's
            var book = _mongoRepository.FindMultipleByIdAsync(request.Ids);

            return book;
        }
    }
}

