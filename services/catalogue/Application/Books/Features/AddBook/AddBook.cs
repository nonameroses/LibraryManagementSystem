using Application.Books.Dtos;
using Domain.Entities;
using Infrastructure.Data.Repositories;
using MapsterMapper;
using MediatR;

namespace Application.Books.Features.AddBook;

public class AddBook
{
    public sealed class Command : IRequest<BookDto>
    {
        public readonly BookDto Book;

        public Command(BookDto book)
        {
            Book = book;
        }
    }

    public class AddBookCommandHandler : IRequestHandler<Command, BookDto>
    {
        //private readonly IMongoRepository<BookDto, Guid> _context;
        private readonly IBookRepository _repository;
        private readonly IMapper _mapper;

        public AddBookCommandHandler(IBookRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<BookDto> Handle(Command request, CancellationToken cancellationToken)
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