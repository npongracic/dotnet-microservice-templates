using BuildingBlocks.EventBus.Abstractions;
using BuildingBlocks.EventBus.Attributes;
using BuildingBlocks.EventBus.Events;
using Dapr.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingBlocks.EventBus
{
    public class DaprEventBus : IEventBus
    {
        private readonly DaprClient _dapr;
        private readonly ILogger<DaprEventBus> _logger;
        private const string PARTITION_KEY = "partitionKey";

        public DaprEventBus(DaprClient dapr, ILogger<DaprEventBus> logger)
        {
            _dapr = dapr;
            _logger = logger;
        }

        public async Task PublishAsync<TIntegrationEvent>(string topicName, TIntegrationEvent @event, string pubSubName = "pubsub", Dictionary<string, string> metadata = null)
            where TIntegrationEvent : IntegrationEvent
        {
            if (string.IsNullOrWhiteSpace(topicName))
            {
                throw new ArgumentNullException(nameof(topicName));
            }

            if (@event == null)
            {
                throw new ArgumentNullException(nameof(@event));
            }

            var eventType = @event.GetType();
            var partitionKey = eventType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PartitionKeyAttribute))).FirstOrDefault();
           
            _logger.LogInformation("Publishing event {@Event} to {PubsubName}.{TopicName}", @event, pubSubName, topicName);
            @event.EventType = eventType.Name;

            var meta = new Dictionary<string, string>();
            if(partitionKey != null)
            {
                meta.TryAdd(PARTITION_KEY, partitionKey.GetValue(@event)?.ToString());
            }

            if(metadata != null)
            {
                foreach (var key in metadata.Keys)
                {
                    meta.TryAdd(key, metadata[key]);
                }
            }

            // We need to make sure that we pass the concrete type to PublishEventAsync,
            // which can be accomplished by casting the event to dynamic. This ensures
            // that all event fields are properly serialized.
            await _dapr.PublishEventAsync(pubSubName, topicName, (dynamic)@event, meta);
        }
    }
}
