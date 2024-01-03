using Domain.Entities;
//using MapsterMapper;
using MediatR;

namespace Application.Books.Features.Commands;

public class AddBook
{
    public sealed class Command : IRequest<Book>
    {
        public readonly Book Book;

        public Command(Book book)
        {
            Book = book;
        }
    }

    public class Handler : IRequestHandler<Command, Book>
    {
        private readonly IMongoRepository<Book> _bookRepository;

        public Handler(IMongoRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Book> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = new Book
            {
                Title = request.Book.Title,
                Author = request.Book.Author,
                Isbn = request.Book.Isbn,
                Quantity = request.Book.Quantity
            };

            await _bookRepository.InsertOneAsync(entity);

            return entity;
            //return _mapper.Map<BookDto>(entity);
        }
    }
}