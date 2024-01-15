using Domain.Entities;
using MediatR;
using MongoDB.Bson;

namespace Application.Books.Features.Commands;
public class DeleteBook
{
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
        private readonly IMongoRepository<Book> _mongoRepository;

        public Handler(IMongoRepository<Book> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            await _mongoRepository.DeleteOneAsync(
                filter => filter.Author == request.Author ||
                                                                   filter.Title == request.Title ||
                                                                   filter.Isbn == request.Isbn);
        }
    }
}
