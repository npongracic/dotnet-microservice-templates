﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using SC.API.CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System;
using SC.API.CleanArchitecture.Domain.Enums;
using System.Collections.Generic;
using System.Linq;

namespace SC.API.CleanArchitecture.Infrastructure.Persistence.Configurations
{
    public class IdentificationSchemaConfiguration : IEntityTypeConfiguration<IdentificationSchema>
    {
        public void Configure(EntityTypeBuilder<IdentificationSchema> entity)
        {
            entity.ToTable("IdentificationSchema");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .ValueGeneratedNever();

            entity.Property(e => e.GlobalId)
                .HasColumnName("GlobalID")
                 .ValueGeneratedOnAdd().HasDefaultValue(Guid.NewGuid());

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(e => e.Uri)
                .HasColumnName("URI")
                .HasMaxLength(2048);

            var enumValues = new List<int>((int[])Enum.GetValues(typeof(IdentificationSchemaEnum)));

            entity.HasData(enumValues.Select(t => new IdentificationSchema() {
                Id = t,
                Name = ((IdentificationSchemaEnum)t).ToString()
            }));
        }
    }
}
