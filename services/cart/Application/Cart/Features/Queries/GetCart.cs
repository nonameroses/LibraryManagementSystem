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
        public string Title { get; set; }
        public string Author { get; set; }
        public int Isbn { get; set; }

        public Query(string title, string author, int isbn)
        {
            Title = title;
            Author = author;
            Isbn = isbn;
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
            var book = _mongoRepository.FindOneAsync(
                filter => filter.Author == request.Author ||
                          filter.Title == request.Title ||
                          filter.Isbn == request.Isbn
            );

            return await book;
        }
    }
}
