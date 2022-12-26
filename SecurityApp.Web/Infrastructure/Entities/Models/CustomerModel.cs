using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations;

namespace SecurityApp.Web.Infrastructure.Entities.Models
{
    public class CustomerModel
    {
        public Guid Id { get; set; }

        [Required]        
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        
        [Required]
        [EmailAddress]
        [MaxLength(250)]        
        public string Mail { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        [Required]
        public int AuthAttempts { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public bool Block { get; set; }
    }

    public class CustomerConfig : IEntityTypeConfiguration<CustomerModel>
    {
        public void Configure(EntityTypeBuilder<CustomerModel> builder)
        {
            builder.ToTable("CUSTOMER");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("CUSTOMER");
            builder.Property(e => e.FirstName).HasColumnName("FIRST_NAME").IsRequired().HasMaxLength(50);
            builder.Property(e => e.LastName).HasColumnName("LAST_NAME").IsRequired().HasMaxLength(100);
            builder.Property(e => e.Mail).HasColumnName("MAIL").IsRequired().HasMaxLength(250);
            builder.Property(e => e.Password).HasColumnName("PASSWORD").IsRequired().HasMaxLength(50);
            builder.Property(e => e.AuthAttempts).HasColumnName("AUTH_ATTEMPTS").IsRequired();
            builder.Property(e => e.Active).HasColumnName("ACTIVE").IsRequired();
            builder.Property(e => e.Block).HasColumnName("BLOCK").IsRequired();

            builder.HasIndex(e => e.Mail).IsUnique();
        }
    }
}
