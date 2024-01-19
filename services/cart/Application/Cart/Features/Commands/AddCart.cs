using Domain.Entities;
using FluentValidation;
using MediatR;
using MongoDB.Bson;

namespace Application.Cart.Features.Commands;

public class AddCart
{
    public sealed class Command : IRequest<CustomerCart>
    {
        public readonly CustomerCart Cart;

        public Command(CustomerCart cart)
        {
            Cart = cart;
        }
    }
    public sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {


            // Rule for FirstName - not empty, max length 50 characters, only letters and spaces
            RuleFor(p => p.Cart.FirstName)
                .NotEmpty()
                .MaximumLength(50)
                .Matches(@"^[a-zA-Z\s]*$").WithMessage("'FirstName should only contain letters.")
                .WithName("FirstName")
                .WithMessage("FirstName name cannot be empty and can only contain letters!");

            // Rule for LastName - not empty, max length 100 characters, only letters spaces and numbers
            RuleFor(p => p.Cart.LastName)
                .NotEmpty()
                .MaximumLength(100)
                .Matches(@"^[a-zA-Z\s]*$").WithMessage("'LastName should only contain letters.")
                .WithName("LastName")
                .WithMessage("LastName name cannot be empty and can only contain letters!");

            // Rule fo Quantity - not empty, can only be a number more than 0
            RuleFor(p => p.Cart.Order[0].Quantity)
                .GreaterThanOrEqualTo(1)
                .WithName("Quantity")
                .WithMessage("Quantity Number cannot be 0 or less!");

        }
    }

    public class Handler : IRequestHandler<Command, CustomerCart>
    {
        private readonly IMongoRepository<CustomerCart> _mongoRepository;

        public Handler(IMongoRepository<CustomerCart> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<CustomerCart> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = new CustomerCart
            {
                Id = ObjectId.GenerateNewId().ToString(),
                FirstName = request.Cart.FirstName,
                LastName = request.Cart.LastName,
                Order = request.Cart.Order,
            };

            await _mongoRepository.InsertOneAsync(entity);

            return entity;
        }
    }
}