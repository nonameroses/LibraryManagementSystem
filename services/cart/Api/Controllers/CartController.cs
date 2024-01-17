using Application.Cart.Features.Commands;
using Application.Cart.Features.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CartController : ControllerBase
{
    private readonly IMediator _mediator;

    public CartController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("addCart")]
    public async Task<IActionResult> AddCart(CustomerCart request)
    {
        var cart = new CustomerCart()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Order = request.Order
        };

        await _mediator.Send(new AddCart.Command(cart));
        return CreatedAtAction(nameof(GetCart), new { id = cart.Id }, cart);
    }

    [HttpGet("getCarts")]
    public async Task<IEnumerable<CustomerCart>> GetCarts()
    {
        var books = await _mediator.Send(new GetCarts.Query());

        return books;
    }
    [HttpGet("getCart")]
    public async Task<CustomerCart> GetCart(string firstName, string lastName)
    {
        var result = await _mediator.Send(new GetCart.Query(firstName, lastName));

        return result;
    }
    [HttpGet("getOrderItems")]
    public async Task<IEnumerable<OrderItem>> GetOrderItems(string firstName, string lastName)
    {
        var result = await _mediator.Send(new GetCartOrders.Query(firstName, lastName));

        return result;
    }
    //[HttpPut("updateCart")]
    //public async Task<IActionResult> UpdateCart(Book request)
    //{
    //    //var book = await _mediator.Send(new GetBook.Query(request.Title, request.Author, request.Isbn));

    //    var result = await _mediator.Send(new UpdateBook.Command(request));

    //    return NoContent();
    //}
    //[HttpDelete("deleteCart")]
    //public async Task<IActionResult> DeleteCart(string firstName, string lastName, int isbn)
    //{
    //    await _mediator.Send(new DeleteBook.Command(firstName, lastName, isbn));

    //    return NoContent();
    //}
}