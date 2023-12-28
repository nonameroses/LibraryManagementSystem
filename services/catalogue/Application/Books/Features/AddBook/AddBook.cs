using Application.Books.Dtos;
using Domain.Entities;
using MapsterMapper;
using MediatR;

namespace Application.Books.Features.AddBook;

public class AddBook
{
    public sealed class AddBookCommand : IRequest<BookDto>
    {
        public readonly BookDto Book;

        public AddBookCommand(BookDto book)
        {
            Book = book;
        }
    }

    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, BookDto>
    {
        //private readonly IMongoRepository<BookDto, Guid> _context;
        private readonly IBookRepository _repository;
        private readonly IMapper _mapper;

        public AddBookCommandHandler(IBookRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<BookDto> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            var entity = new Book
            {
                Name = request.Book.Name,
                Author = request.Book.Author,
                Title = request.Book.Title,
                Isbn = request.Book.Isbn,
                Quantity = request.Book.Quantity
            };

           await _repository.AddAsync(entity);

           return _mapper.Map<BookDto>(entity);
        }
    }
}