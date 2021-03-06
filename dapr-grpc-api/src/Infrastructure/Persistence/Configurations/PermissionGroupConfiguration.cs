// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using SC.API.CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System;

namespace SC.API.CleanArchitecture.Infrastructure.Persistence.Configurations
{
    public class PermissionGroupConfiguration : IEntityTypeConfiguration<PermissionGroup>
    {
        public void Configure(EntityTypeBuilder<PermissionGroup> entity)
        {
            entity.ToTable("PermissionGroup");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .ValueGeneratedNever();

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
