using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SampleController : ControllerBase
{
    private readonly IMongoRepository<Book> _peopleRepository;

    public SampleController(IMongoRepository<Book> peopleRepository)
    {
        _peopleRepository = peopleRepository;
    }

    [HttpPost("registerBook")]
    public async Task AddPerson(string firstName, string lastName)
    {
        var book = new Book()
        {
            Author = "Pyder"
        };

        await _peopleRepository.InsertOneAsync(book);
    }

    [HttpGet("getPeopleData")]
    public IEnumerable<string> GetPeopleData()
    {
        var people = _peopleRepository.FilterBy(
            filter => filter.Author != "test",
            projection => projection.Author
        );
        return people;
    }
}