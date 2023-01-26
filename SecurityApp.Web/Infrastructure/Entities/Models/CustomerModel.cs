using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecurityApp.Web.Infrastructure.Entities.DTO;
using System;
using System.ComponentModel.DataAnnotations;

namespace SecurityApp.Web.Infrastructure.Entities.Models
{
    public class CustomerModel
    {
        public CustomerModel() { }

        public CustomerModel(CustomerLeadDTO pEntity)
        {
            Id = Guid.NewGuid();
            FirstName = pEntity.FirstName;
            LastName = null;
            Mail = pEntity.Mail;
            Password = pEntity.Password;
            AuthAttempts = 0;
            Active = true;
            Block = false;
        }

        public Guid Id { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        [Required]      
        [MaxLength(50)]
        public string FirstName { get; set; }

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

        public override bool Equals(object obj)
        {
            return obj is CustomerModel model &&
                   Id.Equals(model.Id) &&
                   CreationDate == model.CreationDate &&
                   UpdateDate == model.UpdateDate &&
                   FirstName == model.FirstName &&
                   LastName == model.LastName &&
                   Mail == model.Mail &&
                   Password == model.Password &&
                   AuthAttempts == model.AuthAttempts &&
                   Active == model.Active &&
                   Block == model.Block;
        }
    }

    public class CustomerConfig : IEntityTypeConfiguration<CustomerModel>
    {
        public void Configure(EntityTypeBuilder<CustomerModel> builder)
        {
            builder.ToTable("CUSTOMER");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("CUSTOMER");
            builder.Property(e => e.CreationDate).HasColumnName("CREATION_DATE");
            builder.Property(e => e.UpdateDate).HasColumnName("UPDATE_DATE");
            builder.Property(e => e.FirstName).HasColumnName("FIRST_NAME").IsRequired().HasMaxLength(50);
            builder.Property(e => e.LastName).HasColumnName("LAST_NAME").HasMaxLength(100);
            builder.Property(e => e.Mail).HasColumnName("MAIL").IsRequired().HasMaxLength(250);
            builder.Property(e => e.Password).HasColumnName("PASSWORD").IsRequired().HasMaxLength(50);
            builder.Property(e => e.AuthAttempts).HasColumnName("AUTH_ATTEMPTS").IsRequired();
            builder.Property(e => e.Active).HasColumnName("ACTIVE").IsRequired();
            builder.Property(e => e.Block).HasColumnName("BLOCK").IsRequired();

            builder.HasIndex(e => e.Mail).IsUnique();
        }
    }
}
