using Domain.Entities;
using MediatR;
using MongoDB.Bson;

namespace Application.Books.Features.Commands;
public class UpdateBook
{
    public sealed class Command : IRequest<Book>
    {
        public readonly Book Book;
        public readonly ObjectId Id;

        public Command(Book book, ObjectId id)
        {
            Book = book;
            Id = id;
        }
    }

    public class Handler : IRequestHandler<Command, Book>
    {
        private readonly IMongoRepository<Book> _mongoRepository;

        public Handler(IMongoRepository<Book> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<Book> Handle(Command request, CancellationToken cancellationToken)
        {
            var book = _mongoRepository.FindOne(
                filter => filter.Author == request.Book.Author ||
                          filter.Title == request.Book.Title ||
                          filter.Isbn == request.Book.Isbn
            );


            var entity = new Book
            {
                Title = request.Book.Title,
                Author = request.Book.Author,
                Isbn = request.Book.Isbn,
                Quantity = request.Book.Quantity,
                Id = request.Id
            };
            //entityToUpdate = entity;

            await _mongoRepository.ReplaceOneAsync(entity);

            return entity;
            //return _mapper.Map<BookDto>(entity);
        }
    }
}
