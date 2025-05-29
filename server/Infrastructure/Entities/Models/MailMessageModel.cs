using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Infrastructure.Entities.Models
{
    public class MailMessageModel
    { 
        public MailMessageModel() { }

        public MailMessageModel(Guid id, Guid idCustomer, DateTime? creationDate, DateTime? sendDate, string title, string body)
        {
            Id = id;
            IdCustomer = idCustomer;
            CreationDate = creationDate;
            SendDate = sendDate;
            Title = title;
            Body = body;
        }

        [Required]
        public Guid Id { get; protected set; }

        [Required]
        public Guid IdCustomer { get; protected set; }

        public DateTime? CreationDate { get; protected set; }

        public DateTime? SendDate { get; protected set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; protected set; }

        [Required]        
        public string Body { get; protected set; }

        [NotMapped]
        public CustomerModel Customer { get; protected set; }

        public override bool Equals(object obj)
        {
            return obj is MailMessageModel model &&
                   Id.Equals(model.Id) &&
                   IdCustomer.Equals(model.IdCustomer) &&
                   CreationDate == model.CreationDate &&
                   SendDate == model.SendDate &&
                   Title == model.Title &&
                   Body == model.Body;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, IdCustomer, CreationDate, SendDate, Title, Body);
        }
    }

    public class MailMessageConfig : IEntityTypeConfiguration<MailMessageModel>
    {
        public void Configure(EntityTypeBuilder<MailMessageModel> builder)
        {
            builder.ToTable("MAIL_MESSAGE");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("ID_MAIL_MESSAGE").IsRequired();
            builder.Property(e => e.IdCustomer).HasColumnName("ID_CUSTOMER").IsRequired();
            builder.Property(e => e.CreationDate).HasColumnName("CREATION_DATE").IsRequired();
            builder.Property(e => e.SendDate).HasColumnName("SEND_DATE");
            builder.Property(e => e.Title).HasColumnName("TITLE").IsRequired().HasMaxLength(100);
            builder.Property(e => e.Body).HasColumnName("BODY").IsRequired();
        }
    }
}

