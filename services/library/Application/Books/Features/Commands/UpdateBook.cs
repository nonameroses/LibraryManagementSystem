using Domain.Entities;
using FluentValidation;
using MediatR;

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
    // FluentValidation package allows easy validation for models
    public sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            // Rule for Author - not empty, max length 50 characters, only letters and spaces
            RuleFor(p => p.Book.Author)
                .NotEmpty()
                .MaximumLength(50)
                .Matches(@"^[A-Za-z\s]*$").WithMessage("'Author should only contain letters.")
                .WithName("Author")
                .WithMessage("Author name cannot be empty!");

            // Rule for Title - not empty, max length 100 characters, only letters and spaces
            RuleFor(p => p.Book.Title)
                .NotEmpty()
                .MaximumLength(100)
                .Matches(@"^[A-Za-z\s]*$").WithMessage("'Title should only contain letters.")
                .WithName("Title")
                .WithMessage("Title name cannot be empty!");

            // Rule fo Isbn - not empty, can only be a number more than 0
            RuleFor(p => p.Book.Isbn)
                .GreaterThanOrEqualTo(1)
                .WithName("Isbn")
                .WithMessage("International Standard Book Number cannot be 0!");

            //RuleFor(p => p.Book.Quantity)
            //    .GreaterThanOrEqualTo(1)
            //    .WithName("Quantity")
            //    .WithMessage("Quantity must be more than 0!");

        }
    }

    // Handler. IRequestHandler Takes in Command as a request and returns Book as a response
    public class Handler : IRequestHandler<Command, Book>
    {
        // Declare Mongo Repo class to use the methods
        private readonly IMongoRepository<Book> _mongoRepository;
        // RabbitMq Producer class
        public Handler(IMongoRepository<Book> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<Book> Handle(Command request, CancellationToken cancellationToken)
        {
            // Maybe change by id  ?
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
        }
    }
}
