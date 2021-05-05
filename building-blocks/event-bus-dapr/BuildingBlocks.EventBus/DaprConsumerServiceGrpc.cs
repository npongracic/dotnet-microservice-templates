using BuildingBlocks.EventBus.Events;
using Dapr.AppCallback.Autogen.Grpc.v1;
using Dapr.Client;
using Dapr.Client.Autogen.Grpc.v1;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BuildingBlocks.EventBus
{
    public class DaprConsumerServiceGrpc : AppCallback.AppCallbackBase
    {
        private readonly ILogger<DaprConsumerServiceGrpc> _logger;
        private readonly DaprClient _daprClient;
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="daprClient"></param>
        /// <param name="logger"></param>
        public DaprConsumerServiceGrpc(DaprClient daprClient, ILogger<DaprConsumerServiceGrpc> logger, IMediator mediator)
        {
            _daprClient = daprClient;
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// implement OnIvoke to support getaccount, deposit and withdraw
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<InvokeResponse> OnInvoke(InvokeRequest request, ServerCallContext context)
        {
            return await Task.FromResult(new InvokeResponse());
        }

        /// <summary>
        /// implement ListTopicSubscriptions to register deposit and withdraw subscriber
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<ListTopicSubscriptionsResponse> ListTopicSubscriptions(Empty request, ServerCallContext context)
        {
            var result = new ListTopicSubscriptionsResponse();
            //result.Subscriptions.Add(new TopicSubscription
            //{
            //    PubsubName = "pubsub",
            //    Topic = "orders"
            //});
            //result.Subscriptions.Add(new TopicSubscription
            //{
            //    PubsubName = "pubsub",
            //    Topic = "withdraw"
            //});

            return Task.FromResult(result);
        }

        private static System.Type GetEventByName(string name, ILogger<DaprConsumerServiceGrpc> logger)
        {
            try
            {
                // We could cache found implementations here
                var foundType = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(x =>
                                    !x.IsInterface && !x.IsAbstract && x.Name == name && x?.BaseType == typeof(IntegrationEvent)).FirstOrDefault();

                logger.LogDebug("Found an implementation for the event type [{eventType}]: {foundType}", name, foundType);
                return foundType;
            }
            catch (Exception ex)
            {
                logger.LogError("Error while loading assemblies for [{eventType}]: {exception}", name, ex);
            }

            return null;
        }

        /// <summary>
        /// implement OnTopicEvent to handle integration events
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<TopicEventResponse> OnTopicEvent(TopicEventRequest request, ServerCallContext context)
        {
            var result = new TopicEventResponse();
            result.Status = TopicEventResponse.Types.TopicEventResponseStatus.Success;

            var data = request.Data.ToStringUtf8();
            var input = JsonConvert.DeserializeObject<IntegrationEvent>(data);
            var eventType = GetEventByName(input.EventType, _logger);

            if (eventType != null)
            {
                try
                {
                    var @event = JsonConvert.DeserializeObject(data, eventType);
                    await _mediator.Publish(@event);
                    _logger.LogInformation("Dispached integration event {eventName} to found implementation {eventType}", input.EventType, eventType);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error while dispatching integration event {eventName}: {error}", input.EventType, ex);
                    result.Status = TopicEventResponse.Types.TopicEventResponseStatus.Retry;
                }
            }
            else
            {
                result.Status = TopicEventResponse.Types.TopicEventResponseStatus.Drop;
            }

            return await Task.FromResult(result);
        }




    }
}
