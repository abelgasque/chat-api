using ChatApi.Domain.Entities.Tenants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApi.Domain.Entities.Configs
{    public class ChatMessageModelConfig : IEntityTypeConfiguration<ChatMessageModel>
    {
        public void Configure(EntityTypeBuilder<ChatMessageModel> builder)
        {
            builder.ToTable("ChatMessages");

            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(m => m.ExternalId)
                .HasColumnName("EXTERNAL_ID")
                .IsRequired();

            builder.Property(m => m.ChatId)
                .HasColumnName("CHAT_ID")
                .IsRequired();

            builder.Property(m => m.Timestamp)
                .HasColumnName("TIMESTAMP")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(m => m.Body)
                .HasColumnName("BODY")
                .IsRequired();

            builder.Property(m => m.FromMe)
                .HasColumnName("FROM_ME")
                .IsRequired();

            builder.Property(m => m.Source)
                .HasColumnName("SOURCE")
                .IsRequired(false);

            builder.Property(m => m.HasMedia)
                .HasColumnName("HAS_MEDIA")
                .IsRequired();

            builder.Property(m => m.Ack)
                .HasColumnName("ACK")
                .IsRequired();

            builder.Property(m => m.AckName)
                .HasColumnName("ACK_NAME")
                .IsRequired(false);

            builder.Property(m => m.Engine)
                .HasColumnName("ENGINE")
                .IsRequired(false);

            builder.Property(m => m.SenderId)
                .HasColumnName("SENDER_ID")
                .IsRequired(false);

            builder.Property(m => m.SenderName)
                .HasColumnName("SENDER_NAME")
                .IsRequired(false);
        }
    }
}
