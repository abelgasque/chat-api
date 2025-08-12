using ChatApi.Domain.Entities.Tenants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApi.Domain.Entities.Configs
{    public class ChatMessageModelConfig : IEntityTypeConfiguration<ChatMessageModel>
    {
        public void Configure(EntityTypeBuilder<ChatMessageModel> builder)
        {
            builder.ToTable("CHAT_MESSAGES");

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

            builder.Property(m => m.ChatId)
                .HasColumnName("CHAT_ID")
                .IsRequired();

            builder.Property(m => m.Message)
                .HasColumnName("MESSAGE")
                .HasMaxLength(5000)
                .IsRequired();
        }
    }
}
