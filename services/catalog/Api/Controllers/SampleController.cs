using Domain.Entities;
using Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using Application.Books.Features.AddBooks;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SampleController : ControllerBase
{
    // private readonly IMongoRepository<Book> _peopleRepository;
    private readonly IMediator _mediator;

    public SampleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("registerBook")]
    public async Task AddPerson(string firstName, string lastName)
    {
        var book = new Book()
        {
            Author = firstName
        };
        
        await _mediator.Send(new AddBook.Command(book));

    }

    //[HttpGet("getPeopleData")]
    //public IEnumerable<string> GetPeopleData()
    //{
    //    var people = _peopleRepository.FilterBy(
    //        filter => filter.Author != "test",
    //        projection => projection.Author
    //    );
    //    return people;
    //}
}