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
    public class PartyRoleTypeConfiguration : IEntityTypeConfiguration<PartyRoleType>
    {
        public void Configure(EntityTypeBuilder<PartyRoleType> entity)
        {
            entity.ToTable("PartyRoleType");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .ValueGeneratedNever();

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.ParentClassId).HasColumnName("ParentClassID");

            entity.Property(e => e.PartyRoleTypeDiscriminatorId).HasColumnName("PartyRoleTypeDiscriminatorID");

            entity.HasOne(d => d.ParentClass)
                .WithMany(p => p.InverseParentClass)
                .HasForeignKey(d => d.ParentClassId)
                .HasConstraintName("FK_PartyRoleType_PartyRoleType");

            entity.HasOne(d => d.PartyRoleTypeDiscriminator)
                .WithMany(p => p.PartyRoleTypes)
                .HasForeignKey(d => d.PartyRoleTypeDiscriminatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PartyRoleType_PartyRoleTypeDiscriminator");

            var enumValues = new List<int>((int[])Enum.GetValues(typeof(PartyRoleTypeEnum)));

            entity.HasData(enumValues.Select(t => new PartyRoleType()
            {
                Id = t,
                Name = ((PartyRoleTypeEnum)t).ToString(),
                PartyRoleTypeDiscriminatorId = GetDescriminator((PartyRoleTypeEnum)t)
            }));
        }

        private int GetDescriminator(PartyRoleTypeEnum partyRoleType)
        {
            switch (partyRoleType) {
                case PartyRoleTypeEnum.ServiceProvider:
                    return (int)PartyRoleTypeDiscriminatorEnum.OrganizationPartyRoleTypes;
                case PartyRoleTypeEnum.PlatformOwner:
                    return (int)PartyRoleTypeDiscriminatorEnum.OrganizationPartyRoleTypes;
                case PartyRoleTypeEnum.Employee:
                    return (int)PartyRoleTypeDiscriminatorEnum.IndividualPartyRoleTypes;
                case PartyRoleTypeEnum.Employer:
                    return (int)PartyRoleTypeDiscriminatorEnum.OrganizationPartyRoleTypes;
                case PartyRoleTypeEnum.Member:
                    return (int)PartyRoleTypeDiscriminatorEnum.IndividualPartyRoleTypes;
                case PartyRoleTypeEnum.Team:
                    return (int)PartyRoleTypeDiscriminatorEnum.OrganizationPartyRoleTypes;
                case PartyRoleTypeEnum.TeamMember:
                    return (int)PartyRoleTypeDiscriminatorEnum.IndividualPartyRoleTypes;
                case PartyRoleTypeEnum.Organization:
                    return (int)PartyRoleTypeDiscriminatorEnum.OrganizationPartyRoleTypes;
                case PartyRoleTypeEnum.OrganizationDepartment:
                    return (int)PartyRoleTypeDiscriminatorEnum.OrganizationPartyRoleTypes;
                case PartyRoleTypeEnum.Tenant:
                    return (int)PartyRoleTypeDiscriminatorEnum.OrganizationPartyRoleTypes;
                case PartyRoleTypeEnum.OrganizationPost:
                    return (int)PartyRoleTypeDiscriminatorEnum.IndividualPartyRoleTypes;
                case PartyRoleTypeEnum.OrganizationExpertTeam:
                    return (int)PartyRoleTypeDiscriminatorEnum.OrganizationPartyRoleTypes;
                case PartyRoleTypeEnum.OrganizationSpecialistTeam:
                    return (int)PartyRoleTypeDiscriminatorEnum.OrganizationPartyRoleTypes;
                case PartyRoleTypeEnum.Customer:
                    return (int)PartyRoleTypeDiscriminatorEnum.OrganizationPartyRoleTypes;
                case PartyRoleTypeEnum.Partner:
                    return (int)PartyRoleTypeDiscriminatorEnum.OrganizationPartyRoleTypes;
                case PartyRoleTypeEnum.Supplier:
                    return (int)PartyRoleTypeDiscriminatorEnum.OrganizationPartyRoleTypes;
                case PartyRoleTypeEnum.Buyer:
                    return (int)PartyRoleTypeDiscriminatorEnum.OrganizationPartyRoleTypes;
                case PartyRoleTypeEnum.Competitor:
                    return (int)PartyRoleTypeDiscriminatorEnum.OrganizationPartyRoleTypes;
                case PartyRoleTypeEnum.Vendor:
                    return (int)PartyRoleTypeDiscriminatorEnum.OrganizationPartyRoleTypes;
                case PartyRoleTypeEnum.Intermediary:
                    return (int)PartyRoleTypeDiscriminatorEnum.OrganizationPartyRoleTypes;
                case PartyRoleTypeEnum.ServiceProviderServiceAccount:
                    return (int)PartyRoleTypeDiscriminatorEnum.OrganizationPartyRoleTypes;
                case PartyRoleTypeEnum.MemberBusinessAdministrator:
                    return (int)PartyRoleTypeDiscriminatorEnum.IndividualPartyRoleTypes;
                case PartyRoleTypeEnum.MemberBusinessApprover:
                    return (int)PartyRoleTypeDiscriminatorEnum.IndividualPartyRoleTypes;
                case PartyRoleTypeEnum.OrganizationPostCEO:
                    return (int)PartyRoleTypeDiscriminatorEnum.IndividualPartyRoleTypes;
                default:
                    throw new InvalidOperationException("PartyRoleTypeConfiguration invalid descriminator");
            }
        }
    }
}