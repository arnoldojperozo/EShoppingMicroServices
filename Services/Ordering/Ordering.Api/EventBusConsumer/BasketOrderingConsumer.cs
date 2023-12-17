using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Ordering.Application.Commands;

namespace Ordering.Api.EventBusConsumer;

public class BasketOrderingConsumer : IConsumer<BasketCheckoutEvent>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<BasketOrderingConsumer> _logger;

    public BasketOrderingConsumer(IMediator mediator, ILogger<BasketOrderingConsumer> logger, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        //using var scope = _logger.BeginScope("Consuming Basket Checkout Event for {correlationId}",
        //    context.Message.CorrelationId);

        var command = _mapper.Map<CheckoutOrderCommand>(context.Message);

        await _mediator.Send(command);

        _logger.LogInformation($"Basket Checkout Event Completed");
    }
}
