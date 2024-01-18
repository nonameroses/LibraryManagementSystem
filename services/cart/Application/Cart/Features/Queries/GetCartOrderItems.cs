using Application.Producer;
using Domain;
using Domain.Entities;
using MediatR;

namespace Application.Cart.Features.Queries;

//Query to get a single book 
public class GetCartOrders
{
    public sealed class Query : IRequest<IEnumerable<string>>
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

    public class Handler : IRequestHandler<Query, IEnumerable<string>>
    {
        private readonly IMongoRepository<CustomerCart> _mongoRepository;
        private readonly IOrderItemsMessageProducer _producer;

        public Handler(IMongoRepository<CustomerCart> mongoRepository, IOrderItemsMessageProducer producer)
        {
            _mongoRepository = mongoRepository;
            _producer = producer;
        }

        public async Task<IEnumerable<string>> Handle(Query request, CancellationToken cancellationToken)
        {
            // return a single book if any of the filter matches
            var cart = await _mongoRepository.FindOneAsync(
                filter => filter.FirstName == request.FirstName ||
                          filter.LastName == request.LastName
            );

            //Only get the list of ID's, do not take the Quantity
            var orderItems = cart.Order.Select(x => x.Id);
            // Produce orderItems message
            _producer.ProduceItemsMessage(orderItems);
            // Return ID's
            return orderItems;
        }
    }
}
