using Domain.Entities;
using MediatR;

namespace Application.Books.Features.Queries;

//Query to get a single book by its title or author or isbn
public class GetBook
{
    public sealed class Query : IRequest<Book>
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
                filter => filter.Author == request.Author ||
                           filter.Title == request.Title ||
                           filter.Isbn == request.Isbn
                );

            return await book;
        }
    }
}
