using System.Net;
using Basket.Application.Commands;
using Basket.Application.GrpcService;
using Basket.Application.Mappers;
using Basket.Application.Queries;
using Basket.Application.Responses;
using Basket.Core.Entities;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers;

public class BasketController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly DiscountGrpcService _discountGrpcService;

    public BasketController(IMediator mediator, IPublishEndpoint publishEndpoint)
    {
        _mediator = mediator;
        _publishEndpoint = publishEndpoint;
    }

    [HttpGet]
    [Route("[action]/{userName}", Name = "GetBasketByUserName")]
    [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCartResponse>> GetBasket(string userName)
    {
        var query = new GetBasketByUserNameQuery(userName);

        var basket = await _mediator.Send(query);

        return Ok(basket);
    }

    [HttpDelete("DeleteBasketByUserName")]
    [Route("[action]/{userName}", Name = "DeleteBasketByUserName")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCartResponse>> DeleteBasketByUserName(string userName)
    {
        var query = new DeleteBasketByUserNameQuery(userName);

        await _mediator.Send(query);

        return Ok();
    }

    [HttpPost("CreateBasket")]
    [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCartResponse>> UpdateBasket([FromBody] CreateShoppingCartCommand createShoppingCartCommand)
    {
        var basket = await _mediator.Send(createShoppingCartCommand);

        return Ok(basket);
    }

    [Route("[action]")]
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
    {
        //Get Existing basket with UserName
        var query = new GetBasketByUserNameQuery(basketCheckout.UserName);
        var basket=await _mediator.Send(query);

        if(basket is null)
            return BadRequest();

        var eventMsg=BasketMapper.Mapper.Map<BasketCheckout>(basketCheckout);
        eventMsg.TotalPrice=basket.TotalPrice;

        //Publish Message in Queue
        await _publishEndpoint.Publish<BasketCheckout>(eventMsg);

        //Remove once published
        var deleteQuery = new DeleteBasketByUserNameQuery(basketCheckout.UserName);

        await _mediator.Send(deleteQuery);

        return Accepted();
    }
}
