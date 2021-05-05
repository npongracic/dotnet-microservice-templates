﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using SC.API.CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System;

namespace SC.API.CleanArchitecture.Infrastructure.Persistence.Configurations
{
    public class ContactMediumConfiguration : IEntityTypeConfiguration<ContactMedium>
    {
        public void Configure(EntityTypeBuilder<ContactMedium> entity)
        {
            entity.ToTable("ContactMedium");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.ContactMediumClassId).HasColumnName("ContactMediumClassID");

            entity.Property(e => e.Value)
                .IsRequired()
                .HasMaxLength(2048);

            entity.HasOne(d => d.ContactMediumClass)
                .WithMany(p => p.ContactMedia)
                .HasForeignKey(d => d.ContactMediumClassId)
                .HasConstraintName("FK_ContactMedium_ContactMediumClass");
        }
    }
}