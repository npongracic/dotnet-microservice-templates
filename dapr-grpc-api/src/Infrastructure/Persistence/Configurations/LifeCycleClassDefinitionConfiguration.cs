﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using SC.API.CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using SC.API.CleanArchitecture.Domain.Enums;
using System.Linq;

namespace SC.API.CleanArchitecture.Infrastructure.Persistence.Configurations
{
    public class LifeCycleClassDefinitionConfiguration : IEntityTypeConfiguration<LifeCycleClassDefinition>
    {
        public void Configure(EntityTypeBuilder<LifeCycleClassDefinition> entity)
        {
            entity.ToTable("LifeCycleClassDefinition");

            entity.HasKey(t => t.Id);

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .ValueGeneratedNever();

            entity.Property(e => e.ClassName)
                .IsRequired()
                .HasMaxLength(50);

            var enumValues = new List<int>((int[])Enum.GetValues(typeof(LifeCycleClassDefinitionEnum)));

            entity.HasData(enumValues.Select(t => new LifeCycleClassDefinition()
            {
                Id = t,
                ClassName = ((LifeCycleClassDefinitionEnum)t).ToString()
            }));
        }
    }
}