using Domain.Entities;
using FluentValidation;
using MediatR;
using MongoDB.Bson;

namespace Application.Books.Features.Commands;
public class UpdateBook
{
    public sealed class Command : IRequest<Book>
    {
        public readonly Book Book;


        public Command(Book book)
        {
            Book = book;

        }
    }
    public sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(p => p.Book.Author)
                .NotEmpty()
                .MaximumLength(50)
                .WithName("Author")
                .WithMessage("Author name cannot be empty!");

            RuleFor(p => p.Book.Title)
                .NotEmpty()
                .MaximumLength(50)
                .WithName("Title")
                .WithMessage("Title name cannot be empty!");

            RuleFor(p => p.Book.Isbn)
                .GreaterThanOrEqualTo(1)
                .WithName("Cost")
                .WithMessage("International Standard Book Number cannot be 0!");

            RuleFor(p => p.Book.Quantity)
                .GreaterThanOrEqualTo(1)
                .WithName("Quantity")
                .WithMessage("Quantity must be more than 0!");

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
                Id = book.Id
            };
            //entityToUpdate = entity;

            await _mongoRepository.ReplaceOneAsync(entity);

            return entity;
            //return _mapper.Map<BookDto>(entity);
        }
    }
}
