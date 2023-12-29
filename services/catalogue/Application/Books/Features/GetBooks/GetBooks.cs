using Application.Books.Dtos;
using Domain.Entities;

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

    public class GetBooksQueryHandler 
    {
       // private readonly IBookRepository _repository;

        public GetBooksQueryHandler()
        {
         //   _repository = repository;
        }

     
    }
}