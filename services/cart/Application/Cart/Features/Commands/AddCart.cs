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
        //    RuleFor(p => p.Cartt.FirstName)
        //        .NotEmpty()
        //        .MaximumLength(50)
        //        .WithName("FirstName")
        //        .WithMessage("FirstName name cannot be empty!");

        //    RuleFor(p => p.Cartt.LastName)
        //        .NotEmpty()
        //        .MaximumLength(50)
        //        .WithName("Title")
        //        .WithMessage("Title name cannot be empty!");

        //    RuleFor(p => p.Cartt.Email)
        //        .GreaterThanOrEqualTo(1)
        //        .WithName("Isbn")
        //        .WithMessage("International Standard Book Number cannot be 0!");

        //    RuleFor(p => p.Cartt.Quantity)
        //        .GreaterThanOrEqualTo(1)
        //        .WithName("Quantity")
        //        .WithMessage("Quantity must be more than 0!");

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
                Orders = request.Cart.Orders,
            };

            await _mongoRepository.InsertOneAsync(entity);

            return entity;
        }
    }
}