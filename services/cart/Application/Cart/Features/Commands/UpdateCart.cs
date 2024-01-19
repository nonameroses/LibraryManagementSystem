using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Cart.Features.Commands;
public class UpdateCart
{
    public sealed class Command : IRequest<CustomerCart>
    {
        public readonly CustomerCart Cart;


        public Command(CustomerCart cart)
        {
            Cart = cart;
        }
    }
    // FluentValidation package allows easy validation for models
    public sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            // Rule for Author - not empty, max length 50 characters, only letters and spaces
            RuleFor(p => p.Cart.FirstName)
                .NotEmpty()
                .MaximumLength(50)
                .Matches(@"^[A-Za-z\s]*$").WithMessage("'FirstName should only contain letters.")
                .WithName("FirstName")
                .WithMessage("FirstName name cannot be empty!");

            // Rule for Title - not empty, max length 100 characters, only letters and spaces
            RuleFor(p => p.Cart.LastName)
                .NotEmpty()
                .MaximumLength(100)
                .Matches(@"^[A-Za-z\s]*$").WithMessage("'Title should only contain letters.")
                .WithName("Title")
                .WithMessage("Title name cannot be empty!");

            RuleFor(p => p.Cart.Order[0].Quantity)
                .GreaterThanOrEqualTo(1)
                .WithName("Quantity")
                .WithMessage("Quantity must be more than 0!");

        }
    }

    // Handler. IRequestHandler Takes in Command as a request and returns Cart as a response
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
            var cart = _mongoRepository.FindOne(
                filter => filter.FirstName == request.Cart.FirstName ||
                          filter.LastName == request.Cart.LastName
            );

            var entity = new CustomerCart
            {
                FirstName = request.Cart.FirstName,
                LastName = request.Cart.LastName,
                Order = request.Cart.Order,
                Id = cart.Id
            };

            await _mongoRepository.ReplaceOneAsync(entity);

            return entity;
        }
    }
}

