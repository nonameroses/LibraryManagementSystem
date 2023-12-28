using Application.Books.Dtos;
using Application.Books.Features.AddBook;
using Application.Books.Features.GetBooks;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[Controller]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IMediator _mediator;

    public BooksController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    public async Task<List<Book>> Get()
    {
        return await _mediator.Send(new GetBooks.GetBooksQuery());
    }
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] BookDto request)
    {
        var command = new AddBook.Command(request);
        var commandResponse = await _mediator.Send(command);

        return CreatedAtAction(nameof(Get), new { commandResponse.Id }, commandResponse);
    }

}