using Domain.Entities;
using MediatR;
using System.Diagnostics.Metrics;
using FluentValidation;

namespace Application.Books.Features.Queries;

//Query to get a single book by Id 
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

    // Handler. IRequestHandler Takes in Query as a request and returns Book as a response
    public class Handler : IRequestHandler<Query, Book>
    {
        // Declare Mongo Repo class to use the methods
        private readonly IMongoRepository<Book> _mongoRepository;

        // Injecting dependency into constructor
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

