using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ChatApi.Domain.Entities.Models;

namespace ChatApi.Domain.Entities.Configs
{
    public class TenantModelConfig : IEntityTypeConfiguration<TenantModel>
    {
        public void Configure(EntityTypeBuilder<TenantModel> builder)
        {
            builder.ToTable("TENANTS");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("TENANT_ID")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Guid)
                .HasColumnName("TENANT_GUID")
                .IsRequired();

            builder.Property(e => e.Name)
                .HasColumnName("TENANT_NAME")
                .IsRequired();

            builder.Property(e => e.CreatedAt)
                .HasColumnName("TENANT_CREATED_AT")
                .IsRequired();

            builder.Property(e => e.UpdatedAt)
                .HasColumnName("TENANT_UPDATED_AT");

            builder.Property(e => e.DeletedAt)
                .HasColumnName("TENANT_DELETED_AT");

            builder.Property(e => e.Domain)
                .HasColumnName("TENANT_DOMAIN")
                .IsRequired();

            builder.Property(e => e.Database)
                .HasColumnName("TENANT_DATABASE")
                .IsRequired();
        }
    }
}