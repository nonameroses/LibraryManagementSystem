using Domain.Entities;
using FluentValidation;
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
    public sealed class Validator : AbstractValidator<Command>
    {
        //public Validator()
        //{
        //    RuleFor(p => p.Book.Author).NotEmpty().WithMessage("KA NX");
        //}
        public Validator(IMongoRepository<Book> _repository)
        {
            RuleFor(p => p.Book.Author)
                .NotEmpty()
                .MaximumLength(75)
                .WithName("Name");

            RuleFor(p => p.Book.Quantity)
                .GreaterThanOrEqualTo(1)
                .WithName("Cost");

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
        }
    }
}