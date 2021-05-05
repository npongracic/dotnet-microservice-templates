using BuildingBlocks.EventBus.Abstractions;
using BuildingBlocks.EventBus.Attributes;
using BuildingBlocks.EventBus.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.API.Web.IntegrationEvents
{
    public class OrderStartedEvent : IntegrationEvent
    {
        [PartitionKey]
        public string ProductName { get; set; }
        public int Quantity { get; set; }

        public class OrderStartedEventHandler : IIntegrationEventHandler<OrderStartedEvent>
        {
            private readonly IMediator _mediator;
            public OrderStartedEventHandler(IMediator mediator)
            {
                _mediator = mediator;
            }

            public async Task Handle(OrderStartedEvent notification, CancellationToken cancellationToken)
            {

            }

        }
    }
}
