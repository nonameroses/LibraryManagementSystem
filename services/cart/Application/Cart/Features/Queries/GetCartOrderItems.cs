using Domain;
using Domain.Entities;
using MediatR;

namespace Application.Cart.Features.Queries;

//Query to get a single book 
public class GetCartOrders
{
    public sealed class Query : IRequest<IEnumerable<OrderItem>>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public List<OrderItem> Order { get; set; }

        public Query(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }

    //public sealed class Validator : AbstractValidator<Query>
    //{
    //    public Validator()
    //    {
    //        RuleFor(p => p.Author).NotEmpty().WithMessage("KA NX");
    //    }
    //}

    public class Handler : IRequestHandler<Query, IEnumerable<OrderItem>>
    {
        private readonly IMongoRepository<CustomerCart> _mongoRepository;

        public Handler(IMongoRepository<CustomerCart> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<IEnumerable<OrderItem>> Handle(Query request, CancellationToken cancellationToken)
        {
            // return a single book if any of the filter matches
            var cart = await _mongoRepository.FindOneAsync(
                filter => filter.FirstName == request.FirstName ||
                          filter.LastName == request.LastName
            );

            var orderItems = cart.Order;

            return orderItems;
        }
    }
}
