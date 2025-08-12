using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApi.Domain.Entities.Models
{
    public class UserModelConfig : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.ToTable("USERS");

            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(e => e.AvatarUrl)
                .HasColumnName("AVATAR_URL")
                .HasMaxLength(500);

            builder.Property(e => e.Name)
                .HasColumnName("NAME")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(e => e.Email)
                .HasColumnName("EMAIL")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(e => e.Phone)
                .HasColumnName("PHONE")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(e => e.Password)
                .HasColumnName("PASSWORD")
                .HasMaxLength(255);

            builder.Property(e => e.NuLogged)
                .HasColumnName("NU_LOGGED")
                .IsRequired();

            builder.Property(e => e.LoggedAt)
                .HasColumnName("LOGGED_AT");

            builder.Property(e => e.NuRefreshed)
                .HasColumnName("NU_REFRESHED")
                .IsRequired();

            builder.Property(e => e.RefreshedAt)
                .HasColumnName("REFRESHED_AT");

            builder.Property(e => e.ActiveAt)
                .HasColumnName("ACTIVE_AT");

            builder.Property(e => e.BlockedAt)
                .HasColumnName("BLOCKED_AT");

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
