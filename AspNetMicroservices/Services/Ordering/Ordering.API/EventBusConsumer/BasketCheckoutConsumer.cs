﻿using AutoMapper;
using EventBus.Message.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Features.Commands.CheckoutOrder;
using System.Threading.Tasks;

namespace Ordering.API.EventBusConsumer
{
    public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ILogger<BasketCheckoutConsumer> _logger;

        // please make sure you are using using MediatR; not MassTransit.mediator
        public BasketCheckoutConsumer(IMapper mapper, IMediator mediator, ILogger<BasketCheckoutConsumer> logger)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            var command = _mapper.Map<CheckoutOrderCommand>(context.Message);
            var result = await _mediator.Send(command);
            _logger.LogInformation("BasketCheckoutEvent consumed successfully. Created Order Id: {newOrderId}", result);
        }
    }
}
