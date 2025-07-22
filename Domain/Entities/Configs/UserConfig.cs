using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ChatApi.Domain.Entities.Models;

namespace ChatApi.Domain.Entities.Models
{
    public class UserModelConfig : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.ToTable("USERS");

            builder.HasIndex(e => e.Email).IsUnique();

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("USER_ID")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Guid)
                .HasColumnName("USER_GUID")
                .IsRequired();

            builder.Property(e => e.Name)
                .HasColumnName("USER_NAME")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(e => e.Email)
                .HasColumnName("USER_EMAIL")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(e => e.Password)
                .HasColumnName("USER_PASSWORD")
                .HasMaxLength(255);

            builder.Property(e => e.NuLogged)
                .HasColumnName("USER_NU_LOGGED")
                .IsRequired();

            builder.Property(e => e.LoggedAt)
                .HasColumnName("USER_LOGGED_AT");

            builder.Property(e => e.NuRefreshed)
                .HasColumnName("USER_NU_REFRESHED")
                .IsRequired();

            builder.Property(e => e.RefreshedAt)
                .HasColumnName("USER_REFRESHED_AT");

            builder.Property(e => e.ActiveAt)
                .HasColumnName("USER_ACTIVE_AT");

            builder.Property(e => e.BlockedAt)
                .HasColumnName("USER_BLOCKED_AT");

            builder.Property(e => e.CreatedAt)
                .HasColumnName("USER_CREATED_AT")
                .IsRequired();

            builder.Property(e => e.UpdatedAt)
                .HasColumnName("USER_UPDATED_AT");

            builder.Property(e => e.DeletedAt)
                .HasColumnName("USER_DELETED_AT");
        }
    }
}
