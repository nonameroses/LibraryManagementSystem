using Domain.Entities;
using MediatR;

namespace Application.Books.Features.Commands;

// Command Class for Deleting a book
public class DeleteBook
{
    // Command using MediatR to represent a request with a response
    public sealed class Command : IRequest
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Isbn { get; set; }

        public Command(string title, string author, int isbn)
        {
            Title = title;
            Author = author;
            Isbn = isbn;
        }
    }

    public class Handler : IRequestHandler<Command>
    {
        // Declare Mongo Repo class to use the methods
        private readonly IMongoRepository<Book> _mongoRepository;

        // Inject the dependency into constructor
        public Handler(IMongoRepository<Book> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        // Handler for deleting a book 
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            // Calls mongo repo class to delete an object and filters through the collection using this logic
            await _mongoRepository.DeleteOneAsync(
                filter => filter.Author == request.Author ||
                                                                   filter.Title == request.Title ||
                                                                   filter.Isbn == request.Isbn);
        }
    }
}
