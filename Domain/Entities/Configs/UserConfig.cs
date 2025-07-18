using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ChatApi.Domain.Entities.Models;

namespace ChatApi.Domain.Entities.Models
{
    public class UserConfig : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.ToTable("USERS");

            builder.HasIndex(e => e.Mail).IsUnique();

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("USER_ID")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.guid)
                .HasColumnName("USER_GUID")
                .IsRequired();

            builder.Property(e => e.Name)
                .HasColumnName("USER_NAME")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.Mail)
                .HasColumnName("USER_MAIL")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(e => e.Password)
                .HasColumnName("USER_PASSWORD")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.AuthAttempts)
                .HasColumnName("USER_AUTH_ATTEMPTS")
                .IsRequired();

            builder.Property(e => e.ActiveAt)
                .HasColumnName("USER_ACTIVE_AT");

            builder.Property(e => e.BlockedAt)
                .HasColumnName("USER_BLOCKED_AT");
        }
    }
}
