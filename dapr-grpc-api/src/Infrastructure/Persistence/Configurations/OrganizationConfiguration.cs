// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using SC.API.CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System;

namespace SC.API.CleanArchitecture.Infrastructure.Persistence.Configurations
{
    public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> entity)
        {
            entity.ToTable("Organization");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .ValueGeneratedNever();

            entity.Property(e => e.Code).HasMaxLength(100);

            entity.Property(e => e.TradingName).HasMaxLength(510);


            //entity.Property(e => e.Level).HasComputedColumnSql(@"coalesce(len(Code) - len(replace(Code,'-','')), 0)", stored: true);

            entity.HasOne(d => d.Party)
                .WithOne(p => p.Organization)
                .HasForeignKey<Organization>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Organization_Party");

           
        }
    }
}
