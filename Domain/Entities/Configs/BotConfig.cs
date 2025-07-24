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
                .HasColumnName("ID")
                .IsRequired()
                .ValueGeneratedOnAdd();

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

            builder.Property(e => e.Code)
                .HasColumnName("CODE");
        }
    }
}