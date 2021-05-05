using SC.API.CleanArchitecture.Domain.Entities;
using SC.API.CleanArchitecture.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace SC.API.CleanArchitecture.Infrastructure.Persistence.Configurations
{
    public class CatalogConfiguration : IEntityTypeConfiguration<Catalog>
    {
        private const string _catalogPrefix = "Cat";

        public void Configure(EntityTypeBuilder<Catalog> builder)
        {
            builder.Property(e => e.UserFriendlyName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.SystemName)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasOne(d => d.ParentCatalog)
             .WithMany(p => p.Catalogs)
             .HasForeignKey(d => d.ParentCatalogId)
             .OnDelete(DeleteBehavior.ClientSetNull)
             .HasConstraintName("FK_Catalog_ParentCatalog");

            builder.HasData(new Catalog[] {
                    new Catalog
                    {
                        Id = new Guid("878853a7-f4d9-474d-8515-766ba0ee2dd9"),
                        SystemName = $"{_catalogPrefix}Currencies",
                        UserFriendlyName = "Currencies"
                    },
            });
        }
    }
}
