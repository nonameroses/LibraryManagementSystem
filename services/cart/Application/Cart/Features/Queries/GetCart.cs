using Domain.Entities;
using MediatR;
using System.Diagnostics.Metrics;
using FluentValidation;

namespace Application.Cart.Features.Queries;

//Query to get a single book 
public class GetCart
{
    public sealed class Query : IRequest<CustomerCart>
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

    public class Handler : IRequestHandler<Query, CustomerCart>
    {
        private readonly IMongoRepository<CustomerCart> _mongoRepository;

        public Handler(IMongoRepository<CustomerCart> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<CustomerCart> Handle(Query request, CancellationToken cancellationToken)
        {
            // return a single book if any of the filter matches
            var cart = _mongoRepository.FindOneAsync(
                filter => filter.FirstName == request.FirstName ||
                          filter.LastName == request.LastName
            );


            return await cart;
        }
    }
}
