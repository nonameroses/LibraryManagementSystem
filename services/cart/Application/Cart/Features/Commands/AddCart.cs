using Domain.Entities;
using FluentValidation;
using MediatR;

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
        private readonly IMongoRepository<CustomerCart> _cartRepository;

        public Handler(IMongoRepository<CustomerCart> bookRepository)
        {
            _cartRepository = bookRepository;
        }

        public async Task<CustomerCart> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = new CustomerCart
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