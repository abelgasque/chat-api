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

            builder.HasIndex(m => m.Database)
                .IsUnique()
                .HasDatabaseName("IX_TENANTS_DATABASE");

            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(e => e.Name)
                .HasColumnName("NAME")
                .IsRequired();

            builder.Property(e => e.CreatedAt)
                .HasColumnName("CREATED_AT")
                .IsRequired();

            builder.Property(e => e.UpdatedAt)
                .HasColumnName("UPDATED_AT");

            builder.Property(e => e.DeletedAt)
                .HasColumnName("DELETED_AT");

            builder.Property(e => e.Domain)
                .HasColumnName("DOMAIN");

            builder.Property(e => e.Database)
                .HasColumnName("DATABASE")
                .IsRequired();
        }
    }
}