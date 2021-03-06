// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using SC.API.CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System;

namespace SC.API.CleanArchitecture.Infrastructure.Persistence.Configurations
{
    public class DocumentTemplateConfiguration : IEntityTypeConfiguration<DocumentTemplate>
    {
        public void Configure(EntityTypeBuilder<DocumentTemplate> entity)
        {
            entity.ToTable("DocumentTemplate");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .ValueGeneratedNever();

            entity.Property(e => e.Binary).IsRequired();

            entity.Property(e => e.Extension)
                .IsRequired()
                .HasMaxLength(25)
                .IsUnicode(false);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);

            entity.HasOne(d => d.ExtensionNavigation)
                .WithMany(p => p.DocumentTemplates)
                .HasForeignKey(d => d.Extension)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DocumentTemplate_MimeType");
        }
    }
}
