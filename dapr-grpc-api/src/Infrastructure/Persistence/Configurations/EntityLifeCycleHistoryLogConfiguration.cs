// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using SC.API.CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System;

namespace SC.API.CleanArchitecture.Infrastructure.Persistence.Configurations
{
    public class EntityLifeCycleHistoryLogConfiguration : IEntityTypeConfiguration<EntityLifeCycleHistoryLog>
    {
        public void Configure(EntityTypeBuilder<EntityLifeCycleHistoryLog> entity)
        {
            entity.ToTable("EntityLifeCycleHistoryLog");

            entity.HasIndex(e => e.EntityId);

            entity.HasIndex(e => new { e.EntityId, e.TransationId });

            entity.HasIndex(e => new { e.PreviousLifeCycleId, e.CurrentLifeCycleId });

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.CurrentLifeCycleId).HasColumnName("CurrentLifeCycleID");

            entity.Property(e => e.EntityId).HasColumnName("EntityID");

            entity.Property(e => e.EntityInvolvementRoleId).HasColumnName("EntityInvolvementRoleID");

            entity.Property(e => e.PreviousLifeCycleId).HasColumnName("PreviousLifeCycleID");

            entity.Property(e => e.TransationId).HasColumnName("TransationID");

            entity.HasOne(d => d.Entity)
                .WithMany(p => p.EntityLifeCycleHistoryLogs)
                .HasForeignKey(d => d.EntityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntityLifeCycleHistoryLog_Entity");

            entity.HasOne(d => d.LifeCycleTransitionTable)
                .WithMany(p => p.EntityLifeCycleHistoryLogs)
                .HasForeignKey(d => new { d.PreviousLifeCycleId, d.TransationId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntityLifeCycleHistoryLog_LifeCycleTransitionTable");
        }
    }
}
