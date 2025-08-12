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

            builder.Property(e => e.Type)
                .HasColumnName("TYPE")
                .IsRequired();

            builder.Property(e => e.Lang)
                .HasColumnName("LANG");

            builder.Property(e => e.Url)
                .HasColumnName("URL");
        }
    }
}