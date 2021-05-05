using BuildingBlocks.EventBus.Abstractions;
using BuildingBlocks.EventBus.Attributes;
using BuildingBlocks.EventBus.Events;
using Dapr;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SC.API.CleanArchitecture.API.Web.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.API.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntegrationEventController : ControllerBase
    {
        private readonly IEventBus _eventBus;
        public IntegrationEventController(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        [HttpPost("order")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> StartOrder(string productName, int quantity)
        {
            var orderStartedEvent = new OrderStartedEvent
            {
                ProductName = productName,
                Quantity = quantity
            };

            await _eventBus.PublishAsync("orders", orderStartedEvent);

            return Accepted();
        }

        //[HttpPost("OrderStarted")]
        //[Topic("pubsub", "orders")]
        //public async Task<ActionResult> OrderStarted(OrderStartedEvent @event)
        //{
        //    return BadRequest();
        //}
       
    }
}
