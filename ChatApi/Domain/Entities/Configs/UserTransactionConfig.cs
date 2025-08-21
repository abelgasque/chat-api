using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ChatApi.Domain.Entities.Tenants;

namespace ChatApi.Domain.Entities.Configs
{
    public class UserTransactionConfig : IEntityTypeConfiguration<UserTransactionModel>
    {
        public void Configure(EntityTypeBuilder<UserTransactionModel> builder)
        {
            builder.ToTable("USER_TRANSCTION");

            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(t => t.Category)
                .HasColumnName("CATEGORY")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(t => t.Type)
                .HasColumnName("TYPE")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(t => t.PaymentAt)
                .HasColumnName("PAYMENT_AT")
                .HasColumnType("date")
                .IsRequired();

            builder.Property(t => t.ReceiveAt)
                .HasColumnName("RECEIVE_AT")
                .HasColumnType("date")
                .IsRequired();

            builder.Property(t => t.Description)
                .HasColumnName("DESCRIPTION")
                .HasColumnType("text")
                .IsRequired(false);

            builder.Property(t => t.Value)
                .HasColumnName("VALUE")
                .HasColumnType("numeric(12,2)")
                .IsRequired();
        }
    }
}