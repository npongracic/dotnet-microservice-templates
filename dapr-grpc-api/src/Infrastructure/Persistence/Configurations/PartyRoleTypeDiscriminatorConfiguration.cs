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
    public class PartyRoleTypeDiscriminatorConfiguration : IEntityTypeConfiguration<PartyRoleTypeDiscriminator>
    {
        public void Configure(EntityTypeBuilder<PartyRoleTypeDiscriminator> entity)
        {
            entity.ToTable("PartyRoleTypeDiscriminator");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .ValueGeneratedNever();

            entity.Property(e => e.Description).HasMaxLength(255);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);

            var enumValues = new List<int>((int[])Enum.GetValues(typeof(PartyRoleTypeDiscriminatorEnum)));

            entity.HasData(enumValues.Select(t => new PartyRoleTypeDiscriminator() {
                Id = t,
                Name = ((PartyRoleTypeDiscriminatorEnum)t).ToString()
            }));
        }
    }
}
