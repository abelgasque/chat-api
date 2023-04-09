using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System;

namespace SecurityApp.Web.Infrastructure.Entities.Models
{
    public class CustomerRoleModel
    {
        public CustomerRoleModel() { }

        public CustomerRoleModel(Guid id, DateTime? creationDate, DateTime? updateDate, string name, string code, bool active)
        {
            Id = id;
            CreationDate = creationDate;
            UpdateDate = updateDate;
            Name = name;
            Code = code;
            Active = active;
        }

        [Required]
        public Guid Id { get; protected set; }

        public DateTime? CreationDate { get; protected set; }

        public DateTime? UpdateDate { get; protected set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; protected set; }

        [Required]
        [MaxLength(100)]
        public string Code { get; protected set; }

        [Required]
        public bool Active { get; protected set; }

        public override bool Equals(object obj)
        {
            return obj is CustomerRoleModel model &&
                   Id.Equals(model.Id) &&
                   CreationDate == model.CreationDate &&
                   UpdateDate == model.UpdateDate &&
                   Name == model.Name &&
                   Code == model.Code &&
                   Active == model.Active;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, CreationDate, UpdateDate, Name, Code, Active);
        }
    }

    public class CustomerRoleConfig : IEntityTypeConfiguration<CustomerRoleModel>
    {
        public void Configure(EntityTypeBuilder<CustomerRoleModel> builder)
        {
            builder.ToTable("CUSTOMER_ROLE");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("ID_CUSTOMER_ROLE").IsRequired();
            builder.Property(e => e.CreationDate).HasColumnName("CREATION_DATE");
            builder.Property(e => e.UpdateDate).HasColumnName("UPDATE_DATE");
            builder.Property(e => e.Name).HasColumnName("NAME").IsRequired().HasMaxLength(50);
            builder.Property(e => e.Code).HasColumnName("CODE").IsRequired().HasMaxLength(100);
            builder.Property(e => e.Active).HasColumnName("ACTIVE").IsRequired();   
        }
    }
}
