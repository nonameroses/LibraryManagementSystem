using Domain.Entities;
using Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using Application.Books.Features.Commands;
using Application.Books.Features.Queries;
using MongoDB.Bson;

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

    [HttpPost("addBook")]
    public async Task<IActionResult> AddBook(Book request)
    {
        //var book = new Book()
        //{
        //    Title = request.Title,
        //    Author = request.Author,
        //    Isbn = request.Isbn
        //};

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
        var result = await _mediator.Send(new GetBook.Query(title,author,isbn));

        return result;
    }

    [HttpGet("getBookById")]
    public async Task<Book> GetBookById(string id)
    {
        var result = await _mediator.Send(new GetBookById.Query(id));

        return result;
    }
    [HttpPut("updateBook")]
    public async Task<IActionResult> UpdateBook( Book request)
    {
        //var book = await _mediator.Send(new GetBook.Query(request.Title, request.Author, request.Isbn));

        var result = await _mediator.Send(new UpdateBook.Command(request));

        return NoContent();
    }
    [HttpDelete("deleteBook")]
    public async Task<IActionResult> DeleteBook(string title, string author, int isbn)
    {
        await _mediator.Send(new DeleteBook.Command( title,  author, isbn));

        return NoContent();
    }
}