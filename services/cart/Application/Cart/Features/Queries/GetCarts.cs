using Domain;
using MediatR;
using static System.Net.Mime.MediaTypeNames;

namespace Application.Cart.Features.Queries;

//Query to get all the carts 
public class GetBooks
{
    public sealed class Query : IRequest<IEnumerable<tesnx>>
    {
        public List<Domain.Entities.Cart> Carts { get; set; }

        public Query()
        {

        }
    }

    public class Handler : IRequestHandler<Query, IEnumerable<Domain.Entities.Cart>>
    {
        private readonly IMongoRepository<Cart> _mongoRepository;

        public Handler(IMongoRepository<Domain.Entities.Cart> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<IEnumerable<Domain.Entities.Cart>> Handle(Query request, CancellationToken cancellationToken)
        {
            var books = _mongoRepository.AsQueryable();

            return books;
        }
    }
}
