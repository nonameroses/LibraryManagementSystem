using Application.Books.Dtos;
using Domain.Entities;
using Infrastructure.Data.Repositories;
using MediatR;

namespace Application.Books.Features.GetBooks;

public class GetBooks
{
    public sealed record GetBooksQuery : IRequest<List<Book>>
    {
        public GetBooksQuery()
        {
            
        }
    }

    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, List<Book>>
    {
        private readonly IBookRepository _repository;

        public GetBooksQueryHandler(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Book>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetBooksAsync<BookDto>(cancellationToken);
        }
    }
}