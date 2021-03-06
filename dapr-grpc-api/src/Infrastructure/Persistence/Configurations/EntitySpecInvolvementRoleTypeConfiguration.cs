// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using SC.API.CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System;

namespace SC.API.CleanArchitecture.Infrastructure.Persistence.Configurations
{
    public class EntitySpecInvolvementRoleTypeConfiguration : IEntityTypeConfiguration<EntitySpecInvolvementRoleType>
    {
        public void Configure(EntityTypeBuilder<EntitySpecInvolvementRoleType> entity)
        {
            entity.ToTable("EntitySpecInvolvementRoleType");

            entity.HasIndex(e => new { e.EntitySpecificationId, e.InvolvementRoleTypeId })
                .IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.EntitySpecificationId).HasColumnName("EntitySpecificationID");

            entity.Property(e => e.InvolvementRoleTypeId).HasColumnName("InvolvementRoleTypeID");

            entity.HasOne(d => d.EntitySpecification)
                .WithMany(p => p.EntitySpecInvolvementRoleTypes)
                .HasForeignKey(d => d.EntitySpecificationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntitySpecInvolvementRoleType_EntitySpecification");

            entity.HasOne(d => d.InvolvementRoleType)
                .WithMany(p => p.EntitySpecInvolvementRoleTypes)
                .HasForeignKey(d => d.InvolvementRoleTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntitySpecInvolvementRoleType_InvolvementRoleType");
        }
    }
}
