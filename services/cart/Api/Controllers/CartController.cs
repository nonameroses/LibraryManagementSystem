﻿using Application.Cart.Features.Commands;
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
        var cart = await _mediator.Send(new AddCart.Command(request));
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
    public async Task<IEnumerable<string>> GetOrderItems(string firstName, string lastName)
    {
        var result = await _mediator.Send(new GetCartOrders.Query(firstName, lastName));

        return result;
    }
    [HttpPut("updateCart")]
    public async Task<IActionResult> UpdateCart(CustomerCart request)
    {
        var result = await _mediator.Send(new UpdateCart.Command(request));

        return Ok(result);
    }
    [HttpDelete("deleteCart")]
    public async Task<IActionResult> DeleteCart(string id)
    {
        await _mediator.Send(new DeleteCart.Command(id));

        return NoContent();
    }
}