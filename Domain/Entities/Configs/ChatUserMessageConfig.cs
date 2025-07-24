using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ChatApi.Domain.Entities.Tenants;

namespace ChatApi.Domain.Entities.Configs
{
    public class ChatUserMessageModelConfig : IEntityTypeConfiguration<ChatUserMessageModel>
    {
        public void Configure(EntityTypeBuilder<ChatUserMessageModel> builder)
        {
            builder.ToTable("CHAT_USER_MESSAGES");

            builder.HasIndex(m => m.SenderId);
            builder.HasIndex(m => m.ReceiverId);

            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(m => m.SenderId)
                .HasColumnName("SENDER_ID")
                .IsRequired();

            builder.Property(m => m.ReceiverId)
                .HasColumnName("RECEIVER_ID")
                .IsRequired();

            builder.Property(m => m.Message)
                .HasColumnName("MESSAGE")
                .IsRequired();

            builder.Property(m => m.SentAt)
                .HasColumnName("SENT_AT")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(m => m.ReceiverAt)
                .HasColumnName("RECEIVER_AT")
                .IsRequired(false);

            builder.Property(m => m.ReadAt)
                .HasColumnName("READ_AT")
                .IsRequired(false);
        }
    }
}
