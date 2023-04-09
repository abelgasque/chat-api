using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecurityApp.Web.Infrastructure.Entities.DTO;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public CustomerModel(Guid id, Guid idCustomerRole, DateTime? creationDate, DateTime? updateDate, DateTime? passwordTempDate, string firstName, string lastName, string code, string mail, string password, string passwordTemp, int authAttempts, bool active, bool block, bool isNewCustomer)
        {
            Id = id;
            IdCustomerRole = idCustomerRole;
            CreationDate = creationDate;
            UpdateDate = updateDate;
            PasswordTempDate = passwordTempDate;
            FirstName = firstName;
            LastName = lastName;
            Code = code;
            Mail = mail;
            Password = password;
            PasswordTemp = passwordTemp;
            AuthAttempts = authAttempts;
            Active = active;
            Block = block;
            IsNewCustomer = isNewCustomer;
        }

        [Required]
        public Guid Id { get; protected set; }

        [Required]
        public Guid IdCustomerRole { get; protected set; }

        public DateTime? CreationDate { get; protected set; }

        public DateTime? UpdateDate { get; protected set; }

        public DateTime? PasswordTempDate { get; protected set; }

        [Required]      
        [MaxLength(50)]
        public string FirstName { get; protected set; }

        [MaxLength(100)]
        public string LastName { get; protected set; }

        [MaxLength(10)]
        public string Code { get; protected set; }

        [Required]
        [EmailAddress]
        [MaxLength(250)]        
        public string Mail { get; protected set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; protected set; }

        [MaxLength(50)]
        public string PasswordTemp { get; protected set; }

        [Required]
        public int AuthAttempts { get; protected set; }

        [Required]
        public bool Active { get; protected set; }

        [Required]
        public bool Block { get; protected set; }

        [Required]
        public bool IsNewCustomer { get; protected set; }

        [NotMapped]
        public CustomerRoleModel Role { get; protected set; }

        public override bool Equals(object obj)
        {
            return obj is CustomerModel model &&
                   Id.Equals(model.Id) &&
                   IdCustomerRole.Equals(model.IdCustomerRole) &&
                   CreationDate == model.CreationDate &&
                   UpdateDate == model.UpdateDate &&
                   PasswordTempDate == model.PasswordTempDate &&
                   FirstName == model.FirstName &&
                   LastName == model.LastName &&
                   Code == model.Code &&
                   Mail == model.Mail &&
                   Password == model.Password &&
                   PasswordTemp == model.PasswordTemp &&
                   AuthAttempts == model.AuthAttempts &&
                   Active == model.Active &&
                   Block == model.Block &&
                   IsNewCustomer == model.IsNewCustomer;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id);
            hash.Add(IdCustomerRole);
            hash.Add(CreationDate);
            hash.Add(UpdateDate);
            hash.Add(PasswordTempDate);
            hash.Add(FirstName);
            hash.Add(LastName);
            hash.Add(Code);
            hash.Add(Mail);
            hash.Add(Password);
            hash.Add(PasswordTemp);
            hash.Add(AuthAttempts);
            hash.Add(Active);
            hash.Add(Block);
            hash.Add(IsNewCustomer);
            return hash.ToHashCode();
        }

        public void SetId()
        {
            Id= Guid.NewGuid();
        }

        public void SetCreationDate()
        {
            CreationDate = DateTime.Now;
        }

        public void SetUpdateDate()
        {
            UpdateDate = DateTime.Now;
        }

        public void SetBlock(bool block) { 
            Block = block;
        }

        public void SetAuthAttempts(int authAttempts)
        {
            AuthAttempts = authAttempts;
        }
    }

    public class CustomerConfig : IEntityTypeConfiguration<CustomerModel>
    {
        public void Configure(EntityTypeBuilder<CustomerModel> builder)
        {
            builder.ToTable("CUSTOMER");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("ID_CUSTOMER").IsRequired();
            builder.Property(e => e.IdCustomerRole).HasColumnName("ID_CUSTOMER_ROLE").IsRequired();
            builder.Property(e => e.CreationDate).HasColumnName("CREATION_DATE");
            builder.Property(e => e.UpdateDate).HasColumnName("UPDATE_DATE");
            builder.Property(e => e.PasswordTempDate).HasColumnName("PASSWORD_TEMP_DATE");
            builder.Property(e => e.FirstName).HasColumnName("FIRST_NAME").IsRequired().HasMaxLength(50);
            builder.Property(e => e.LastName).HasColumnName("LAST_NAME").HasMaxLength(100);
            builder.Property(e => e.Code).HasColumnName("CODE").HasMaxLength(10);
            builder.Property(e => e.Mail).HasColumnName("MAIL").IsRequired().HasMaxLength(250);
            builder.Property(e => e.Password).HasColumnName("PASSWORD").IsRequired().HasMaxLength(50);
            builder.Property(e => e.PasswordTemp).HasColumnName("PASSWORD_TEMP").HasMaxLength(10);
            builder.Property(e => e.AuthAttempts).HasColumnName("AUTH_ATTEMPTS").IsRequired();
            builder.Property(e => e.Active).HasColumnName("ACTIVE").IsRequired();
            builder.Property(e => e.Block).HasColumnName("BLOCK").IsRequired();
            builder.Property(e => e.IsNewCustomer).HasColumnName("IS_NEW_CUSTOMER").IsRequired();

            builder.HasIndex(e => e.Mail).IsUnique();
        }
    }
}
