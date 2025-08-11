using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ChatApi.Domain.Entities.Tenants;

namespace ChatApi.Domain.Entities.Configs
{
    public class ChatModelConfig : IEntityTypeConfiguration<ChatModel>
    {
        public void Configure(EntityTypeBuilder<ChatModel> builder)
        {
            builder.ToTable("Chats");

            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(c => c.MeId)
                .HasColumnName("ME_ID");

            builder.Property(c => c.MeName)
                .HasColumnName("ME_NAME");

            builder.Property(c => c.MeJid)
                .HasColumnName("ME_JID");

            builder.Property(c => c.ChannelId)
                .HasColumnName("CHANNEL_ID");

            builder.Property(c => c.ContactId)
                .HasColumnName("CONTACT_ID");

            builder.Property(c => c.ContactName)
                .HasColumnName("CONTACT_NAME");
        }
    }
}
