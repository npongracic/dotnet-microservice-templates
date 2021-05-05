using SC.API.CleanArchitecture.Application.Common.Mappings;
using SC.API.CleanArchitecture.Domain.Entities;
using System;

namespace SC.API.CleanArchitecture.Application.Entities.Models
{
    public class ProcessLogItem : IMapFrom<EntityProcessLog>
    {
        public long Id { get; set; }
        public long EntityId { get; set; }
        public DateTime Timestamp { get; set; }
        public string ExecutorName { get; set; }
        public string Note { get; set; }
    }
}
