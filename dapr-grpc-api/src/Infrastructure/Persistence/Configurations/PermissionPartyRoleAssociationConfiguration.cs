﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using SC.API.CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System;

namespace SC.API.CleanArchitecture.Infrastructure.Persistence.Configurations
{
    public class PermissionPartyRoleAssociationConfiguration : IEntityTypeConfiguration<PermissionPartyRoleAssociation>
    {
        public void Configure(EntityTypeBuilder<PermissionPartyRoleAssociation> entity)
        {
            entity.HasKey(e => new { e.PermissionId, e.PartyRoleAssociationId })
                .HasName("PK_dbo.PermissionPartyRoleAssociation");

            entity.ToTable("PermissionPartyRoleAssociation");

            entity.HasIndex(e => e.PartyRoleAssociationId);

            entity.HasIndex(e => e.PermissionId);

            entity.Property(e => e.PermissionId).HasColumnName("PermissionID");

            entity.Property(e => e.PartyRoleAssociationId).HasColumnName("PartyRoleAssociationID");

            entity.HasOne(d => d.PartyRoleAssociation)
                .WithMany(p => p.PermissionPartyRoleAssociations)
                .HasForeignKey(d => d.PartyRoleAssociationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PermissionPartyRoleAssociation_PartyRoleAssociation");

            entity.HasOne(d => d.Permission)
                .WithMany(p => p.PermissionPartyRoleAssociations)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PermissionPartyRoleAssociation_Permission");
        }
    }
}