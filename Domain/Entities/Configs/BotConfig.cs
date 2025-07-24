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

            builder.Property(e => e.Code)
                .HasColumnName("CODE");
        }
    }
}