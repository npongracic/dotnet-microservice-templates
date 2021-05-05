using SC.API.CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace SC.API.CleanArchitecture.Infrastructure.Persistence.Configurations
{
    public class IntegrationOrganizationConfiguration : IEntityTypeConfiguration<IntegrationOrganization>
    {
        public void Configure(EntityTypeBuilder<IntegrationOrganization> builder)
        {
            builder.ToTable("IntegrationOrganizationSync");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.TransactionId)
                .HasColumnName("TransactionID");

            builder.Property(t => t.ExternalId)
                .HasColumnName("ExternalId");
        }
    }
}
