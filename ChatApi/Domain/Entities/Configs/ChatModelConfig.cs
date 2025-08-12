using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ChatApi.Domain.Entities.Tenants;

namespace ChatApi.Domain.Entities.Configs
{
    public class ChatModelConfig : IEntityTypeConfiguration<ChatModel>
    {
        public void Configure(EntityTypeBuilder<ChatModel> builder)
        {
            builder.ToTable("CHATS");

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

            builder.Property(m => m.SenderId)
                .HasColumnName("SENDER_ID")
                .IsRequired();

            builder.Property(m => m.ReceiverId)
                .HasColumnName("RECEIVER_ID")
                .IsRequired();

            builder.Property(e => e.CreatedAt)
                .HasColumnName("CREATED_AT")
                .IsRequired();

            builder.Property(e => e.UpdatedAt)
                .HasColumnName("UPDATED_AT");

            builder.Property(e => e.DeletedAt)
                .HasColumnName("DELETED_AT");
        }
    }
}
