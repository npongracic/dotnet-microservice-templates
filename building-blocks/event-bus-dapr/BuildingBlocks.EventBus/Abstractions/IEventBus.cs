using BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.EventBus.Abstractions
{
    public interface IEventBus
    {
        Task PublishAsync<TIntegrationEvent>(string topicName, TIntegrationEvent @event, string pubSubName = "pubsub", Dictionary<string, string> metadata = null)
           where TIntegrationEvent : IntegrationEvent;
    }
}
