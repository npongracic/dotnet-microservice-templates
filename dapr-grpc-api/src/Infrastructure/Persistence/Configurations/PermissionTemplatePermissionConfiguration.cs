// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using SC.API.CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System;

namespace SC.API.CleanArchitecture.Infrastructure.Persistence.Configurations
{
    public class PermissionTemplatePermissionConfiguration : IEntityTypeConfiguration<PermissionTemplatePermission>
    {
        public void Configure(EntityTypeBuilder<PermissionTemplatePermission> entity)
        {
            entity.HasKey(e => new { e.PermissionTemplateId, e.PermissionId });

            entity.ToTable("PermissionTemplatePermission");

            entity.Property(e => e.PermissionTemplateId).HasColumnName("PermissionTemplateID");

            entity.Property(e => e.PermissionId).HasColumnName("PermissionID");

            entity.HasOne(d => d.Permission)
                .WithMany(p => p.PermissionTemplatePermissions)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PermissionTemplatePermission_Permission");

            entity.HasOne(d => d.PermissionTemplate)
                .WithMany(p => p.PermissionTemplatePermissions)
                .HasForeignKey(d => d.PermissionTemplateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PermissionTemplatePermission_PermissionTemplate");
        }
    }
}
