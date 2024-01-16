using Domain;
using Domain.Entities;
using MediatR;
using static System.Net.Mime.MediaTypeNames;

namespace Application.Cart.Features.Queries;

//Query to get all the carts 
public class GetCarts
{
    public sealed class Query : IRequest<IEnumerable<CustomerCart>>
    {
        public List<CustomerCart> Carts { get; set; }

        public Query()
        {

        }
    }

    public class Handler : IRequestHandler<Query, IEnumerable<CustomerCart>>
    {
        private readonly IMongoRepository<CustomerCart> _mongoRepository;

        public Handler(IMongoRepository<CustomerCart> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<IEnumerable<CustomerCart>> Handle(Query request, CancellationToken cancellationToken)
        {
            var books = _mongoRepository.AsQueryable();

            return books;
        }
    }
}
