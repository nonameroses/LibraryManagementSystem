using Domain.Entities;
using MediatR;
using System.Diagnostics.Metrics;
using FluentValidation;

namespace Application.Books.Features.Queries;

//Query to get a single book 
public class GetBookById
{
    public sealed class Query : IRequest<Book>
    {
        public string Id { get; set; }

        public Query(string id)
        {
            Id = id;
        }
    }

    //public sealed class Validator : AbstractValidator<Query>
    //{
    //    public Validator()
    //    {
    //        RuleFor(p => p.Author).NotEmpty().WithMessage("KA NX");
    //    }
    //}

    public class Handler : IRequestHandler<Query, Book>
    {
        private readonly IMongoRepository<Book> _mongoRepository;

        public Handler(IMongoRepository<Book> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<Book> Handle(Query request, CancellationToken cancellationToken)
        {
            // return a single book if any of the filter matches
            var book = _mongoRepository.FindOneAsync(
                filter => filter.Id == request.Id);

            return await book;
        }
    }
}

