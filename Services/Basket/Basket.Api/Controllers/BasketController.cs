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
#pragma warning disable CS0169 // The field 'BasketController._discountGrpcService' is never used
    private readonly DiscountGrpcService _discountGrpcService;
#pragma warning restore CS0169 // The field 'BasketController._discountGrpcService' is never used

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public BasketController(IMediator mediator, IPublishEndpoint publishEndpoint)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
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
#pragma warning disable CS8604 // Possible null reference argument.
        var query = new GetBasketByUserNameQuery(basketCheckout.UserName);
#pragma warning restore CS8604 // Possible null reference argument.
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
