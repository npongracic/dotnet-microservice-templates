// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using SC.API.CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System;

namespace SC.API.CleanArchitecture.Infrastructure.Persistence.Configurations
{
    public class EntityConfiguration : IEntityTypeConfiguration<Entity>
    {
        public void Configure(EntityTypeBuilder<Entity> entity)
        {
            entity.ToTable("Entity");

            entity.HasIndex(e => new { e.Id, e.EntitySpecificationId })
                .IsUnique();

            entity.HasIndex(e => new { e.Id, e.LifeCycleId, e.IsDeleted });

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.EntitySpecificationId).HasColumnName("EntitySpecificationID");

            entity.Property(e => e.LifeCycleChangeByPartyId).HasColumnName("LifeCycleChangeByPartyID");

            entity.Property(e => e.LifeCycleChangeTime)
                 .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.LifeCycleId).HasColumnName("LifeCycleID");

            entity.Property(e => e.ModifiedByPartyId).HasColumnName("ModifiedByPartyID");

            entity.Property(e => e.ModifiedTimeStamp)
                 .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.RecordId)
                .HasColumnName("RecordID")
                .HasDefaultValue(Guid.NewGuid());

            entity.Property(e => e.RecordedByPartyId).HasColumnName("RecordedByPartyID");

            entity.Property(e => e.RecordedTimeStamp)
                 .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.EntitySpecification)
                .WithMany(p => p.Entities)
                .HasForeignKey(d => d.EntitySpecificationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Entity_EntitySpecification");

            entity.HasOne(d => d.LifeCycleChangeByParty)
                .WithMany(p => p.EntityLifeCycleChangeByParties)
                .HasForeignKey(d => d.LifeCycleChangeByPartyId)
                .HasConstraintName("FK_Entity_Individual1");

            entity.HasOne(d => d.LifeCycle)
                .WithMany(p => p.Entities)
                .HasForeignKey(d => d.LifeCycleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Entity_LifeCycle");

            entity.HasOne(d => d.ModifiedByParty)
                .WithMany(p => p.EntityModifiedByParties)
                .HasForeignKey(d => d.ModifiedByPartyId)
                .HasConstraintName("FK_Entity_Individual2");

            entity.HasOne(d => d.RecordedByParty)
                .WithMany(p => p.EntityRecordedByParties)
                .HasForeignKey(d => d.RecordedByPartyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Entity_Individual");
        }
    }
}
