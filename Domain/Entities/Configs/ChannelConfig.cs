using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ChatApi.Domain.Entities.Models;

namespace ChatApi.Domain.Entities.Configs
{
    public class ChannelModelConfig : IEntityTypeConfiguration<ChannelModel>
    {
        public void Configure(EntityTypeBuilder<ChannelModel> builder)
        {
            builder.ToTable("CHANNELS");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("CHANNEL_ID")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Guid)
                .HasColumnName("CHANNEL_GUID")
                .IsRequired();

            builder.Property(e => e.Name)
                .HasColumnName("CHANNEL_NAME")
                .IsRequired();

            builder.Property(e => e.CreatedAt)
                .HasColumnName("CHANNEL_CREATED_AT")
                .IsRequired();

            builder.Property(e => e.UpdatedAt)
                .HasColumnName("CHANNEL_UPDATED_AT");

            builder.Property(e => e.DeletedAt)
                .HasColumnName("CHANNEL_DELETED_AT");

            builder.Property(e => e.Type)
                .HasColumnName("CHANNEL_TYPE");

            builder.Property(e => e.Lang)
                .HasColumnName("CHANNEL_LANG");

            builder.Property(e => e.Url)
                .HasColumnName("CHANNEL_URL");

            builder.Property(e => e.TenantId)
                .HasColumnName("TENANT_ID");

            builder
                .HasOne(e => e.Tenant)
                .WithMany()
                .HasForeignKey(e => e.TenantId)
                .HasConstraintName("FK_CHANNEL_TENANT")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}