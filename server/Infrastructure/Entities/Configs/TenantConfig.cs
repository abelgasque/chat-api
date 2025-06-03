using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Server.Infrastructure.Entities.Models;

namespace Server.Infrastructure.Entities.Configs
{
    public class TenantModelConfig : IEntityTypeConfiguration<TenantModel>
    {
        public void Configure(EntityTypeBuilder<TenantModel> builder)
        {
            builder.ToTable("TENANT");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("TENANT_ID")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.guid).HasColumnName("TENANT_GUID").IsRequired();
            builder.Property(e => e.CreatedAt).HasColumnName("TENENT_CREATED_AT").IsRequired();
            builder.Property(e => e.UpdatedAt).HasColumnName("TENENT_UPDATED_AT").IsRequired();
            builder.Property(e => e.DeletedAt).HasColumnName("TENENT_DELETED_AT");
            builder.Property(e => e.Name).HasColumnName("TENANT_NAME");
            builder.Property(e => e.Database).HasColumnName("TENANT_DATABASE").IsRequired();
        }
    }
}