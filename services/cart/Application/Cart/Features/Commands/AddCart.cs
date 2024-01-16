using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Cart.Features.Commands;

public class AddCart
{
    public sealed class Command : IRequest<Domain.Entities.Cart>
    {
        public readonly Domain.Entities.Cart Cart;

        public Command(Domain.Entities.Cart cart)
        {
            Cart = cart;
        }
    }
    public sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
        //    RuleFor(p => p.Cart.FirstName)
        //        .NotEmpty()
        //        .MaximumLength(50)
        //        .WithName("FirstName")
        //        .WithMessage("FirstName name cannot be empty!");

        //    RuleFor(p => p.Cart.LastName)
        //        .NotEmpty()
        //        .MaximumLength(50)
        //        .WithName("Title")
        //        .WithMessage("Title name cannot be empty!");

        //    RuleFor(p => p.Cart.Email)
        //        .GreaterThanOrEqualTo(1)
        //        .WithName("Isbn")
        //        .WithMessage("International Standard Book Number cannot be 0!");

        //    RuleFor(p => p.Cart.Quantity)
        //        .GreaterThanOrEqualTo(1)
        //        .WithName("Quantity")
        //        .WithMessage("Quantity must be more than 0!");

        }
    }

    public class Handler : IRequestHandler<Command, Domain.Entities.Cart>
    {
        private readonly IMongoRepository<Domain.Entities.Cart> _cartRepository;

        public Handler(IMongoRepository<Domain.Entities.Cart> bookRepository)
        {
            _cartRepository = bookRepository;
        }

        public async Task<Domain.Entities.Cart> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = new Cart
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