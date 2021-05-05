using SC.API.CleanArchitecture.Domain.Entities;
using System;

namespace SC.API.CleanArchitecture.Infrastructure.Persistence
{
    public static class EntityExtensions
    {
        public static void DeleteEntity(this ApplicationDbContext context, Entity entity)
        {
            entity.IsDeleted = true;
            entity.DeletedDate = DateTime.UtcNow;
        }

        public static void AddProcessLogItem(this ApplicationDbContext context, Entity entity, string note, string executorName, int? operationId = null)
        {
            entity.EntityProcessLogs.Add(new EntityProcessLog {
                ExecutorName = executorName,
                Note = note,
                OperationId = operationId,
                Timestamp = DateTime.UtcNow
            });
        }

        public static void AddProcessLogItem(this ApplicationDbContext context, long entityId, string note, string executorName, int? operationId = null)
        {
            context.EntityProcessLogs.Add(new EntityProcessLog {
                EntityId = entityId,
                ExecutorName = executorName,
                Note = note,
                OperationId = operationId,
                Timestamp = DateTime.UtcNow
            });
        }

    }
}
