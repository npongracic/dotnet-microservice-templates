using SC.API.CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SC.API.CleanArchitecture.Infrastructure.Persistence.Configurations
{
    public class DocumentExtensionConfiguration : IEntityTypeConfiguration<DocumentExtension>
    {
        private List<string> _extensions = new List<string>
        {
            ".7z",
            ".doc",
            ".docx",
            ".jpeg",
            ".jpg",
            ".pdf",
            ".pjp",
            ".pjpeg",
            ".png",
            ".rar",
            ".svg",
            ".xls",
            ".xlsx",
            ".zip"
        };

        public void Configure(EntityTypeBuilder<DocumentExtension> entity)
        {
            entity.HasKey(e => e.Extension);

            entity.ToTable("DocumentExtension");

            entity.Property(e => e.Extension)
                .HasMaxLength(25)
                .IsUnicode(false);

            entity.Property(e => e.CommonName).HasMaxLength(255);

            entity.HasData(_extensions.Select(t => new DocumentExtension
            {
                Extension = t
            }));
        }
    }
}
