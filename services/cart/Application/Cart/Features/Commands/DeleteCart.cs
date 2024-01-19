using Domain.Entities;
using MediatR;

namespace Application.Cart.Features.Commands;

// Command Class for Deleting a cart
public class DeleteCart
{
    // Command using MediatR to represent a request with a response
    public sealed class Command : IRequest
    {
        public string Id { get; set; }

        public Command(string id)
        {
            Id = id;
        }
    }

    public class Handler : IRequestHandler<Command>
    {
        // Declare Mongo Repo class to use the methods
        private readonly IMongoRepository<CustomerCart> _mongoRepository;

        // Inject the dependency into constructor
        public Handler(IMongoRepository<CustomerCart> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        // Handler for deleting a book 
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            // Calls mongo repo class to delete an object and filters through the collection using this logic
            await _mongoRepository.DeleteOneAsync(
                filter => filter.Id == request.Id);
        }
    }
}
