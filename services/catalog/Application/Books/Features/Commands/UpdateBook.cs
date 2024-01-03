using Domain.Entities;
using MediatR;
using MongoDB.Bson;

namespace Application.Books.Features.Commands;
public class UpdateBook
{
    public sealed class Command : IRequest<Book>
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Isbn { get; set; }
        public int Quantity { get; set; }

        public Command(string title, string author, int isbn, int quantity)
        {
            Title = title;
            Author = author;
            Isbn = isbn;
            Quantity = quantity;
        }
    }

    public class Handler : IRequestHandler<Command, Book>
    {
        private readonly IMongoRepository<Book> _mongoRepository;

        public Handler(IMongoRepository<Book> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<Book> Handle(Command request, CancellationToken cancellationToken)
        {


            var entity = new Book
            {
                Title = request.Title,
                Author = request.Author,
                Isbn = request.Isbn,
                Quantity = request.Quantity,
            };
            //entityToUpdate = entity;

            await _mongoRepository.ReplaceOneAsync(entity);

            return entity;
            //return _mapper.Map<BookDto>(entity);
        }
    }
}
