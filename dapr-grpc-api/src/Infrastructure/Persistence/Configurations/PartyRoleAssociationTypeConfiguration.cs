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
    public class PartyRoleAssociationTypeConfiguration : IEntityTypeConfiguration<PartyRoleAssociationType>
    {
        public void Configure(EntityTypeBuilder<PartyRoleAssociationType> entity)
        {
            entity.ToTable("PartyRoleAssociationType");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .ValueGeneratedNever();

            entity.Property(e => e.Description);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);

            var enumValues = new List<int>((int[])Enum.GetValues(typeof(PartyRoleAssociationTypeEnum)));

            entity.HasData(enumValues.Select(t => new PartyRoleAssociationType()
            {
                Id = t,
                Name = ((PartyRoleAssociationTypeEnum)t).ToString()
            }));
        }
    }
}
