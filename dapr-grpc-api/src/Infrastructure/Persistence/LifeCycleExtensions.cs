using SC.API.CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.Infrastructure.Persistence
{
    public static class LifeCycleExtensions
    {
        public static async Task ChangeLifeCycleAsync(this ApplicationDbContext context, Entity entity, int transitionId)
        {
            var nextLifeCycle = await context.GetNextLifeCycleAsync(entity, transitionId);

            context.EntityLifeCycleHistoryLogs.Add(new EntityLifeCycleHistoryLog() {
                CurrentLifeCycleId = nextLifeCycle.Id,
                Entity = entity,
                PreviousLifeCycleId = entity.LifeCycleId,
                TransationId = transitionId,
                TransitionTimeStamp = DateTime.UtcNow
            });

            entity.LifeCycle = nextLifeCycle;
            entity.LifeCycleChangeTime = DateTime.UtcNow;
        }

        public static async Task<LifeCycle> GetNextLifeCycleAsync(this ApplicationDbContext context, Entity entity, int transitionId)
        {
            var nextLifeCycles = await context.LifeCycles
                .Where(t => t.LifeCycleClassDefinition.EntitySpecifications.Any(e => e.Id == entity.EntitySpecificationId) && t.Id == entity.LifeCycleId)
                .SelectMany(t => t.LifeCycleTransitionTableCurrentLifeCycles)
                .Where(t => t.TransitionId == transitionId)
                .Select(t => t.NextLifeCycle)
                .ToListAsync();

            if (nextLifeCycles.Count > 1) {
                throw new InvalidOperationException($"Invalid lifeCycle number in transition: ({nextLifeCycles.Count})");
            }

            if (nextLifeCycles.Count == 0) {
                throw new InvalidOperationException($"Invalid operation.");
            }

            return nextLifeCycles.First();
        }

        public static async Task<LifeCycle> GetNextLifeCycleAsync(this ApplicationDbContext context, int entitySpecificationId, int lifeCycleId, int transitionId)
        {
            var nextLifeCycles = await context.LifeCycles
                .Where(t => t.LifeCycleClassDefinition.EntitySpecifications.Any(e => e.Id == entitySpecificationId) && t.Id == lifeCycleId)
                .SelectMany(t => t.LifeCycleTransitionTableCurrentLifeCycles)
                .Where(t => t.TransitionId == transitionId)
                .Select(t => t.NextLifeCycle)
                .ToListAsync();

            if (nextLifeCycles.Count > 1) {
                throw new InvalidOperationException($"Invalid lifeCycle number in transition: ({nextLifeCycles.Count})");
            }

            if (nextLifeCycles.Count == 0) {
                throw new InvalidOperationException($"Invalid operation.");
            }

            var nextLifeCycle = nextLifeCycles.First();

            return nextLifeCycles.First();
        }
    }
}
