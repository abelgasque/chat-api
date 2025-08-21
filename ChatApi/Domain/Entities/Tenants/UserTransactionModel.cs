using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApi.Domain.Entities.Tenants
{
    [Table("UserTransactions")]
    public class UserTransactionModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public DateTime ReceiveAt { get; set; }

        [Required]
        public DateTime PaymentAt { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Value { get; set; }
    }
}