using Application.Books.Features.Commands;
using Application.Books.Features.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    //  Using the MediatR package interface
    private readonly IMediator _mediator;

    // Injecting dependency in the constructor
    public BooksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("addBook")]
    public async Task<IActionResult> AddBook(Book request)
    {
        // Send comman through Mediator and assign the result into a variable
        // to be able to display in the response body
        var book = await _mediator.Send(new AddBook.Command(request));
        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
    }

    [HttpGet("getBooks")]
    public async Task<IEnumerable<Book>> GetBooks()
    {
        var books = await _mediator.Send(new GetBooks.Query());

        return books;
    }
    [HttpGet("getBook")]
    public async Task<Book> GetBook(string title, string author, int isbn)
    {
        var result = await _mediator.Send(new GetBook.Query(title, author, isbn));

        return result;
    }

    [HttpGet("getBookById")]
    public async Task<Book> GetBookById(string id)
    {
        var result = await _mediator.Send(new GetBookById.Query(id));

        return result;
    }

    [HttpGet("getBooksByIds")]
    public async Task<IEnumerable<Book>> GetBooksByIds([FromQuery] List<string> id)
    {
        var result = await _mediator.Send(new GetBooksById.Query(id));

        return result;
    }
    [HttpPut("updateBook")]
    public async Task<IActionResult> UpdateBook(Book request)
    {

        var result = await _mediator.Send(new UpdateBook.Command(request));

        return Ok(result);
    }
    [HttpDelete("deleteBook")]
    public async Task<IActionResult> DeleteBook(string title, string author, int isbn)
    {
        await _mediator.Send(new DeleteBook.Command(title, author, isbn));

        return NoContent();
    }
}