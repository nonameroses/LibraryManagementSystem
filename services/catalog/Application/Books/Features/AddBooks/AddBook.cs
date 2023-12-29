using Domain.Entities;
using Infrastructure.Data;
//using MapsterMapper;
using MediatR;

namespace Application.Books.Features.AddBooks;

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

    public class AddBookCommandHandler : IRequestHandler<Command, Book>
    {
        private readonly IMongoRepository<Book> _bookRepository;
        //  private readonly IBookRepository _repository;
        //private readonly IMapper _mapper;

        public AddBookCommandHandler(IMongoRepository<Book> bookRepository)
        {
            //  _repository = repository;
            _bookRepository = bookRepository;
        }
        public async Task<Book> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = new Book
            {
                Name = request.Book.Name,
                Author = request.Book.Author,
                Title = request.Book.Title,
                Isbn = request.Book.Isbn,
                Quantity = request.Book.Quantity
            };

            await _bookRepository.AddAsync(entity);

           //return _mapper.Map<BookDto>(entity);
        }
    }
}