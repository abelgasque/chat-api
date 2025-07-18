using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApi.Domain.Entities.Models
{
    [Table("Tenants")]
    public class TenantModel
    {
        public long Id { get; set; }

        [Required]
        public Guid Guid { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; }

        [Required]
        public string Domain { get; set; }

        [Required]
        [MaxLength(255)]
        public string Database { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}