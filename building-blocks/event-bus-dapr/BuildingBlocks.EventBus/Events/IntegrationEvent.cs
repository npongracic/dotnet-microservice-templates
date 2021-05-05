using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.EventBus.Events
{
    public class IntegrationEvent : INotification
    {
        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        public Guid? Id { get; set; }

        public DateTime? CreationDate { get; set; }
        public string EventType { get; set; } 
    }
}
