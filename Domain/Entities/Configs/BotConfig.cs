using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ChatApi.Domain.Entities.Tenants;

namespace ChatApi.Domain.Entities.Configs
{
    public class BotModelConfig : IEntityTypeConfiguration<BotModel>
    {
        public void Configure(EntityTypeBuilder<BotModel> builder)
        {
            builder.ToTable("BOTS");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("BOT_ID")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Guid)
                .HasColumnName("BOT_GUID")
                .IsRequired();

            builder.Property(e => e.Name)
                .HasColumnName("BOT_NAME")
                .IsRequired();

            builder.Property(e => e.CreatedAt)
                .HasColumnName("BOT_CREATED_AT")
                .IsRequired();

            builder.Property(e => e.UpdatedAt)
                .HasColumnName("BOT_UPDATED_AT");

            builder.Property(e => e.DeletedAt)
                .HasColumnName("BOT_DELETED_AT");

            builder.Property(e => e.Code)
                .HasColumnName("BOT_CODE");
        }
    }
}