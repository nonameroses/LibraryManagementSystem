using Application.Books.Dtos;
using MediatR;

namespace Application.Books.Features.GetBooks;

public class GetBooks
{
    public sealed record GetBooksQuery : IRequest<List<BookDto>>
    {
        public GetBooksQuery()
        {
            
        }
    }

    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, List<BookDto>>
    {
        private readonly IBookRepository _repository;

        public GetBooksQueryHandler(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<BookDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetBooksAsync<BookDto>(cancellationToken);
        }
    }
}