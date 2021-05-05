using AutoMapper;
using SC.API.CleanArchitecture.Application.Common;
using SC.API.CleanArchitecture.Application.Common.Interfaces;
using SC.API.CleanArchitecture.Application.Entities.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.Application.Entities.Queries
{
    public class EntityProcessLogQuery : IRequest<List<ProcessLogItem>>
    {
        public EntityProcessLogQuery(long entityId)
        {
            EntityId = entityId;
        }

        public long EntityId { get; }
    }

    public class EntityProcessLogQueryHandler : IRequestHandler<EntityProcessLogQuery, List<ProcessLogItem>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public EntityProcessLogQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<List<ProcessLogItem>> Handle(EntityProcessLogQuery query, CancellationToken cancellationToken)
        {
            var processLog = await _context.Entities
                .Where(t => t.Id == query.EntityId && !t.IsDeleted)
                .WithEntitySecurity(_currentUserService)
                .SelectMany(t => t.EntityProcessLogs)
                //.ProjectTo<ProcessLogItem>(_mapper.ConfigurationProvider)
                .Select(t => new ProcessLogItem
                {
                    Id = t.Id,
                    EntityId = t.EntityId,
                    ExecutorName = t.ExecutorName,
                    Note = t.Note,
                    Timestamp = DateTime.SpecifyKind(t.Timestamp, DateTimeKind.Utc)
                })
                .ToListAsync();

            return processLog;
        }
    }
}
