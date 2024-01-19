using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Cart.Features.Commands;
public class UpdateCart
{
    public sealed class Command : IRequest<CustomerCart>
    {
        public readonly CustomerCart Book;


        public Command(CustomerCart book)
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
            RuleFor(p => p.Book.FirstName)
                .NotEmpty()
                .MaximumLength(50)
                .Matches(@"^[A-Za-z\s]*$").WithMessage("'FirstName should only contain letters.")
                .WithName("FirstName")
                .WithMessage("FirstName name cannot be empty!");

            // Rule for Title - not empty, max length 100 characters, only letters and spaces
            RuleFor(p => p.Book.LastName)
                .NotEmpty()
                .MaximumLength(100)
                .Matches(@"^[A-Za-z\s]*$").WithMessage("'Title should only contain letters.")
                .WithName("Title")
                .WithMessage("Title name cannot be empty!");

            RuleFor(p => p.Book.Order.Capacity)
                .GreaterThanOrEqualTo(1)
                .WithName("Quantity")
                .WithMessage("Quantity must be more than 0!");

        }
    }

    // Handler. IRequestHandler Takes in Command as a request and returns Book as a response
    public class Handler : IRequestHandler<Command, CustomerCart>
    {
        // Declare Mongo Repo class to use the methods
        private readonly IMongoRepository<CustomerCart> _mongoRepository;
        // RabbitMq Producer class
        public Handler(IMongoRepository<CustomerCart> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<CustomerCart> Handle(Command request, CancellationToken cancellationToken)
        {
            var book = _mongoRepository.FindOne(
                filter => filter.FirstName == request.Book.FirstName ||
                          filter.LastName == request.Book.LastName
            );

            var entity = new CustomerCart
            {
                FirstName = request.Book.FirstName,
                LastName = request.Book.LastName,
                Order = request.Book.Order,
            };

            await _mongoRepository.ReplaceOneAsync(entity);

            return entity;
        }
    }
}

