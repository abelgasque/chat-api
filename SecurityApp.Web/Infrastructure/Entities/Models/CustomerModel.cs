using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace SecurityApp.Web.Infrastructure.Entities.Models
{
    public class CustomerModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
    }

    public class CustomerConfig : IEntityTypeConfiguration<CustomerModel>
    {
        public void Configure(EntityTypeBuilder<CustomerModel> builder)
        {
            builder.ToTable("CUSTOMER");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("CUSTOMER");
            builder.Property(e => e.Name).HasColumnName("NAME");
            builder.Property(e => e.Mail).HasColumnName("MAIL");
            builder.Property(e => e.Password).HasColumnName("PASSWORD");
            builder.Property(e => e.Active).HasColumnName("ACTIVE");
        }
    }
}
