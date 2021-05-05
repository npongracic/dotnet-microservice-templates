using BuildingBlocks.EventBus.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BuildingBlocks.EventBus.Abstractions
{
    public interface IIntegrationEventHandler<in TIntegrationEvent> : INotificationHandler<TIntegrationEvent>
            where TIntegrationEvent : IntegrationEvent
    {
        //Task Handle(TIntegrationEvent @event);
    }

    public interface IIntegrationEventHandler 
    {
    }
}
