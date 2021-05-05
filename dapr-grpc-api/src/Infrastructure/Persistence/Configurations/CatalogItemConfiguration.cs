using SC.API.CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace SC.API.CleanArchitecture.Infrastructure.Persistence.Configurations
{
    public class CatalogItemConfiguration : IEntityTypeConfiguration<CatalogItem>
    {
        public void Configure(EntityTypeBuilder<CatalogItem> builder)
        {
            builder.Property(e => e.DeletedDate);

            builder.Property(e => e.Value)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasIndex(e => e.Value);

            builder.HasOne(d => d.Catalog)
                .WithMany(p => p.CatalogItems)
                .HasForeignKey(d => d.CatalogId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CatalogItem_Catalog");

            builder.HasOne(d => d.RecordedByParty)
                .WithMany(p => p.CatalogItems)
                .HasForeignKey(d => d.RecordedByPartyId)
                .HasConstraintName("FK_CatalogItem_Party");


            builder.HasData(new CatalogItem[]
            {
                //Currency
                new CatalogItem { CatalogId = new Guid("878853a7-f4d9-474d-8515-766ba0ee2dd9"), Id = new Guid("cae68d51-0477-4a83-b22a-32b6f7b8733c"), Value = "EUR", SortIndex = 1 },
                new CatalogItem { CatalogId = new Guid("878853a7-f4d9-474d-8515-766ba0ee2dd9"), Id = new Guid("6667ebb9-7be5-4ce8-8f85-89f23fb31ed6"), Value = "HRK", SortIndex = 2 },
                new CatalogItem { CatalogId = new Guid("878853a7-f4d9-474d-8515-766ba0ee2dd9"), Id = new Guid("6d56f3c9-a43b-4260-9f02-43d8db2d8223"), Value = "USD", SortIndex = 3 },
            });
        }
    }
}
