using Application;
using Domain.Entities;
using Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CartController : ControllerBase
{
    private readonly IMediator _mediator;

    public CartController(IMongoRepository<Person> peopleRepository)
    {
        _peopleRepository = peopleRepository;
    }

    [HttpPost("registerPerson")]
    public async Task AddPerson(string firstName, string lastName)
    {
        var person = new Person()
        {
            FirstName = "John",
            LastName = "Doe"
        };

        await _peopleRepository.InsertOneAsync(person);
    }

    [HttpGet("getPeopleData")]
    public IEnumerable<string> GetPeopleData()
    {
        var people = _peopleRepository.FilterBy(
            filter => filter.FirstName != "test",
            projection => projection.FirstName
        );
        return people;
    }
}