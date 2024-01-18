using Application.Producer;
using Domain.Entities;
using FluentValidation;
using MediatR;
using MongoDB.Bson;

namespace Application.Books.Features.Commands;

// Command Class for Adding Books
public class AddBook
{
    // Command using MediatR to represent a request with a response
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
            // No need to validate the ID because it is auto generated with MongoDB


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
        private readonly IBookMessageProducer _messageProducer;

        // Injecting dependencies into constructor
        public Handler(IMongoRepository<Book> mongoRepository, IBookMessageProducer messageProducer)
        {
            _mongoRepository = mongoRepository;
            _messageProducer = messageProducer;
        }

        public async Task<Book> Handle(Command request, CancellationToken cancellationToken)
        {
            // Creating book object to insert
            var book = new Book
            {
                // Generating unique ID 
                Id = ObjectId.GenerateNewId().ToString(),
                Title = request.Book.Title,
                Author = request.Book.Author,
                Isbn = request.Book.Isbn
            };

            // Calling the DB repository to insert
            await _mongoRepository.InsertOneAsync(book);
            // Producing message to the RabbitMq queue
            _messageProducer.ProduceBookMessage(book);
            // returning object to display as a response
            return book;
        }
    }
}