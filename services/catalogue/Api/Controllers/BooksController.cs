using Application.Books.Dtos;
using Application.Books.Features.AddBook;
using Application.Books.Features.GetBooks;
using Domain.Entities;
using Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[Controller]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IMongoRepository<Book> _bookRepository;

    public BooksController(IMongoRepository<Book> bookRepository)
    {
        _bookRepository = bookRepository;
    }

    [HttpPost("registerPerson")]
    public async Task AddPerson(string firstName, string lastName)
    {
        var person = new Book()
        {
            Author = "Harry pydder"
        };

        await _bookRepository.InsertOneAsync(person);
    }

    [HttpGet("getPeopleData")]
    public IEnumerable<string> GetPeopleData()
    {
        var people = _bookRepository.FilterBy(
            filter => filter.Author != "Harry pydder",
            projection => projection.Author
        );
        return people;
    }

}