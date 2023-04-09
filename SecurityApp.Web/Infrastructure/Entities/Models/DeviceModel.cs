using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityApp.Web.Infrastructure.Entities.Models
{
    public class DeviceModel
    {
        public DeviceModel() { }

        public DeviceModel(Guid id, Guid idCustomer, DateTime? creationDate, DateTime expirationDate, DateTime? codeDate, string code, bool online)
        {
            Id = id;
            IdCustomer = idCustomer;
            CreationDate = creationDate;
            ExpirationDate = expirationDate;
            CodeDate = codeDate;
            Code = code;
            Online = online;
        }

        [Required]
        public Guid Id { get; protected set; }

        [Required]
        public Guid IdCustomer { get; protected set; }

        public DateTime? CreationDate { get; protected set; }

        public DateTime ExpirationDate { get; protected set; }

        public DateTime? CodeDate { get; protected set; }

        public string Code { get; protected set; }

        public bool Online { get; protected set; }

        [NotMapped]
        public CustomerModel Customer { get; protected set; }

        public override bool Equals(object obj)
        {
            return obj is DeviceModel model &&
                   Id.Equals(model.Id) &&
                   IdCustomer.Equals(model.IdCustomer) &&
                   CreationDate == model.CreationDate &&
                   ExpirationDate == model.ExpirationDate &&
                   CodeDate == model.CodeDate &&
                   Code == model.Code &&
                   Online == model.Online;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, IdCustomer, CreationDate, ExpirationDate, CodeDate, Code, Online, Customer);
        }
    }

    public class DeviceModelConfig : IEntityTypeConfiguration<DeviceModel>
    {
        public void Configure(EntityTypeBuilder<DeviceModel> builder)
        {
            builder.ToTable("DEVICE");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("ID_DEVICE").IsRequired();
            builder.Property(e => e.IdCustomer).HasColumnName("ID_CUSTOMER").IsRequired();
            builder.Property(e => e.CreationDate).HasColumnName("CREATION_DATE").IsRequired();
            builder.Property(e => e.ExpirationDate).HasColumnName("EXPIRATION_DATE").IsRequired();
            builder.Property(e => e.CodeDate).HasColumnName("CODE_DATE");
            builder.Property(e => e.Code).HasColumnName("CODE");
            builder.Property(e => e.Online).HasColumnName("ONLINE").IsRequired();
        }
    }
}
