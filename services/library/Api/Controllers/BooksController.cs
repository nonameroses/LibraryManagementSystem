
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Books.Features.Commands;
using Application.Books.Features.Queries;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    private readonly IMediator _mediator;

    public BooksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("addCart")]
    public async Task<IActionResult> AddCart(Book request)
    {
        var car = new CustomerCart()
        {

        }

        await _mediator.Send(new AddBook.Command(book));
        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
    }

    [HttpGet("getCarts")]
    public async Task<IEnumerable<Book>> GetCarts()
    {
        var books = await _mediator.Send(new GetBooks.Query());

        return books;
    }
    [HttpGet("getCart")]
    public async Task<Book> GetCart(string title, string author, int isbn)
    {
        var result = await _mediator.Send(new GetBook.Query(title,author,isbn));

        return result;
    }
    [HttpPut("updateCart")]
    public async Task<IActionResult> UpdateCart( Book request)
    {
        //var book = await _mediator.Send(new GetBook.Query(request.Title, request.Author, request.Isbn));

        var result = await _mediator.Send(new UpdateBook.Command(request));

        return NoContent();
    }
    [HttpDelete("deleteCart")]
    public async Task<IActionResult> DeleteCart(string title, string author, int isbn)
    {
        await _mediator.Send(new DeleteBook.Command( title,  author, isbn));

        return NoContent();
    }
}